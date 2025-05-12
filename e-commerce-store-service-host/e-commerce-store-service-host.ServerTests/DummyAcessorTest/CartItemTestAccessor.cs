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
    public class CartItemRepositoryTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            // Use a unique in-memory database for each test
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public async Task GetAllAsync_WhenCartItemsExist_ReturnsAllCartItems()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Electronics",
                Description = "Devices and gadgets"
            };
            var product1 = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Widget A",
                Description = "First widget",
                Price = 9.99m,
                SKU = "WIDGET-A",
                Rating = 4.2f,
                Category = category
            };
            var product2 = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Widget B",
                Description = "Second widget",
                Price = 19.99m,
                SKU = "WIDGET-B",
                Rating = 3.8f,
                Category = category
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "Alice",
                Email = "alice@example.com",
                Password = "pw1",
                Address = "123 A St"
            };
            var cart = new Cart
            {
                CartId = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,
                User = user
            };
            var ci1 = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                Quantity = 1,
                Cart = cart,
                Product = product1
            };
            var ci2 = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                Quantity = 2,
                Cart = cart,
                Product = product2
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Categories.Add(category);
                context.Products.AddRange(product1, product2);
                context.Users.Add(user);
                context.Carts.Add(cart);
                context.CartItems.AddRange(ci1, ci2);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new CartItemAccessor(context);
                var all = (await repo.GetAllAsync()).ToList();

                Assert.AreEqual(2, all.Count, "Should return exactly two cart items");
                CollectionAssert.AreEquivalent(
                    new[] { ci1.CartItemId, ci2.CartItemId },
                    all.Select(ci => ci.CartItemId).ToArray());
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_WhenCartItemExists_ReturnsCartItem()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Books",
                Description = "Printed and digital books"
            };
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "C# in Depth",
                Description = "Programming book",
                Price = 39.95m,
                SKU = "BOOK-CSHARP",
                Rating = 4.9f,
                Category = category
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "Bob",
                Email = "bob@example.com",
                Password = "pw2",
                Address = "456 B Ave"
            };
            var cart = new Cart
            {
                CartId = Guid.NewGuid(),
                CreateDate = new DateTime(2025, 1, 1),
                User = user
            };
            var expected = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                Quantity = 3,
                Cart = cart,
                Product = product
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Categories.Add(category);
                context.Products.Add(product);
                context.Users.Add(user);
                context.Carts.Add(cart);
                context.CartItems.Add(expected);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new CartItemAccessor(context);
                var actual = await repo.GetByIdAsync(expected.CartItemId);

                Assert.IsNotNull(actual, "Should find the cart item by ID");
                Assert.AreEqual(expected.Quantity, actual.Quantity, "Quantity should match");

                // explicitly load navigation props
                await context.Entry(actual).Reference(ci => ci.Cart).LoadAsync();
                await context.Entry(actual).Reference(ci => ci.Product).LoadAsync();

                Assert.IsNotNull(actual.Cart, "Cart should be loaded");
                Assert.AreEqual(cart.CartId, actual.Cart.CartId, "CartId should match");

                Assert.IsNotNull(actual.Product, "Product should be loaded");
                Assert.AreEqual(product.ProductId, actual.Product.ProductId, "ProductId should match");
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenCalled_AddsCartItemAndPersists()
        {
            // Arrange
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Gadgets",
                Description = "Small electronic gadgets"
            };
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Gizmo X",
                Description = "Latest gadget",
                Price = 49.99m,
                SKU = "GIZMO-X",
                Rating = 4.5f,
                Category = category
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "Carol",
                Email = "carol@example.com",
                Password = "pw3",
                Address = "789 C Blvd"
            };
            var cart = new Cart
            {
                CartId = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,
                User = user
            };
            var toAdd = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                Quantity = 5,
                Cart = cart,
                Product = product
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                // seed all parents first
                context.Categories.Add(category);
                context.Products.Add(product);
                context.Users.Add(user);
                context.Carts.Add(cart);
                await context.SaveChangesAsync();

                var repo = new CartItemAccessor(context);
                await repo.AddAsync(toAdd);
            }

            // Assert by using a fresh context
            await using (var context = new AppDbContext(_dbOptions))
            {
                var persisted = await context.CartItems.FindAsync(toAdd.CartItemId);
                Assert.IsNotNull(persisted, "CartItem should have been persisted");
                Assert.AreEqual(5, persisted.Quantity, "Quantity should match");

                await context.Entry(persisted).Reference(ci => ci.Cart).LoadAsync();
                await context.Entry(persisted).Reference(ci => ci.Product).LoadAsync();

                Assert.AreEqual(cart.CartId, persisted.Cart.CartId, "CartId should match");
                Assert.AreEqual(product.ProductId, persisted.Product.ProductId, "ProductId should match");
            }
        }
    }
}
