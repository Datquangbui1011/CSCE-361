using System;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace e_commerce_store_service_host.ServerTests.Accessor
{
    [TestClass]
    public class CartRepositoryTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public async Task GetAllAsync_WhenCartsExist_ReturnsAllCarts()
        {
            // Arrange
            var user = new User
            {
                UserId   = Guid.NewGuid(),
                Name     = "Alice",
                Email    = "alice@example.com",
                Password = "pw1",
                Address  = "123 A St"
            };
            var c1 = new Cart
            {
                CartId     = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,
                User       = user
            };
            var c2 = new Cart
            {
                CartId     = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow.AddDays(1),
                User       = user
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Users.Add(user);
                context.Carts.AddRange(c1, c2);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new CartAccessor(context);
                var all = (await repo.GetAllAsync()).ToList();

                Assert.AreEqual(2, all.Count, "Should return exactly two carts");
                CollectionAssert.AreEquivalent(
                    new[] { c1.CartId, c2.CartId },
                    all.Select(c => c.CartId).ToArray());
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_WhenCartExists_ReturnsCart()
        {
            // Arrange
            var user = new User
            {
                UserId   = Guid.NewGuid(),
                Name     = "Bob",
                Email    = "bob@example.com",
                Password = "pw2",
                Address  = "456 B Ave"
            };
            var expected = new Cart
            {
                CartId     = Guid.NewGuid(),
                CreateDate = new DateTime(2025, 1, 1),
                User       = user
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Users.Add(user);
                context.Carts.Add(expected);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo   = new CartAccessor(context);
                var actual = await repo.GetByIdAsync(expected.CartId);

                Assert.IsNotNull(actual, "Should find the cart by ID");
                Assert.AreEqual(expected.CreateDate, actual.CreateDate, "CreateDate should match");

                // explicitly load the User navigation
                await context.Entry(actual)
                             .Reference(c => c.User)
                             .LoadAsync();

                Assert.IsNotNull(actual.User, "User should be loaded");
                Assert.AreEqual(user.UserId, actual.User.UserId, "UserId should match");
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenCalled_AddsCartAndPersists()
        {
            // Arrange
            var user = new User
            {
                UserId   = Guid.NewGuid(),
                Name     = "Carol",
                Email    = "carol@example.com",
                Password = "pw3",
                Address  = "789 C Blvd"
            };
            var toAdd = new Cart
            {
                CartId     = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,
                User       = user
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                // seed the user so the FK is valid
                context.Users.Add(user);
                await context.SaveChangesAsync();

                var repo = new CartAccessor(context);
                await repo.AddAsync(toAdd);
            }

            // Assert by using a fresh context
            await using (var context = new AppDbContext(_dbOptions))
            {
                var persisted = await context.Carts.FindAsync(toAdd.CartId);
                Assert.IsNotNull(persisted, "Cart should have been persisted");

                // load the User nav property
                await context.Entry(persisted)
                             .Reference(c => c.User)
                             .LoadAsync();

                Assert.IsNotNull(persisted.User, "User should be loaded");
                Assert.AreEqual(user.UserId, persisted.User.UserId, "UserId should match");
            }
        }
    }
}
