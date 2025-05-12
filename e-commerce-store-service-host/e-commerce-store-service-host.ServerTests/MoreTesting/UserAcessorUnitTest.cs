using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;

namespace e_commerce_store_service_host.ServerTests.Accessor
{
    [TestClass]
    public class UserAccessorTests
    {
        private DbContextOptions<AppDbContext> _options;

        [TestInitialize]
        public void Setup()
        {
            // each test gets its own in-memory database
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            // arrange
            var seed = new[]
            {
                new User { Name = "Alice", Email = "alice@example.com", Password = "pw1" },
                new User { Name = "Bob",   Email = "bob@example.com",   Password = "pw2" }
            };
            using (var ctx = new AppDbContext(_options))
            {
                await ctx.Users.AddRangeAsync(seed);
                await ctx.SaveChangesAsync();
            }

            // act
            IEnumerable<User> result;
            using (var ctx = new AppDbContext(_options))
            {
                var accessor = new UserAccessor(ctx);
                result = await accessor.GetAllAsync();
            }

            // assert
            Assert.AreEqual(2, result.Count());
            CollectionAssert.AreEquivalent(
                seed.Select(u => u.Email).ToList(),
                result.Select(u => u.Email).ToList()
            );
        }

        [TestMethod]
        public async Task GetByIdAsync_WhenExists_ReturnsUser()
        {
            // arrange
            var user = new User { Name = "Charlie", Email = "charlie@x.com", Password = "pw3" };
            using (var ctx = new AppDbContext(_options))
            {
                await ctx.Users.AddAsync(user);
                await ctx.SaveChangesAsync();
            }

            // act
            User fetched;
            using (var ctx = new AppDbContext(_options))
            {
                var accessor = new UserAccessor(ctx);
                fetched = await accessor.GetByIdAsync(user.UserId);
            }

            // assert
            Assert.IsNotNull(fetched);
            Assert.AreEqual(user.Email, fetched.Email);
        }

        [TestMethod]
        public async Task AddAsync_PersistsNewUser()
        {
            // arrange
            var newUser = new User { Name = "Dana", Email = "dana@x.com", Password = "pw4" };

            // act
            using (var ctx = new AppDbContext(_options))
            {
                var accessor = new UserAccessor(ctx);
                await accessor.AddAsync(newUser);
            }

            // assert
            using (var ctx = new AppDbContext(_options))
            {
                var fromDb = await ctx.Users.FindAsync(newUser.UserId);
                Assert.IsNotNull(fromDb);
                Assert.AreEqual("dana@x.com", fromDb.Email);
            }
        }

        [TestMethod]
        public async Task Delete_RemovesUser()
        {
            // arrange
            var user = new User { Name = "Eve", Email = "eve@x.com", Password = "pw5" };
            using (var ctx = new AppDbContext(_options))
            {
                await ctx.Users.AddAsync(user);
                await ctx.SaveChangesAsync();
            }

            // act
            using (var ctx = new AppDbContext(_options))
            {
                var accessor = new UserAccessor(ctx);
                accessor.Delete(user);  // async void will run SaveChanges internally
                // Give EF a moment to flush
                await Task.Yield();
            }

            // assert
            using (var ctx = new AppDbContext(_options))
            {
                var found = await ctx.Users.FindAsync(user.UserId);
                Assert.IsNull(found);
            }
        }

        [TestMethod]
        public async Task GetbyEmailAsync_IsCaseInsensitive()
        {
            // arrange
            var user = new User
            {
                Name = "Frank",
                Email = "Frank@Example.COM",
                Password = "pw6"
            };
            using (var ctx = new AppDbContext(_options))
            {
                await ctx.Users.AddAsync(user);
                await ctx.SaveChangesAsync();
            }

            // act
            User fetched;
            using (var ctx = new AppDbContext(_options))
            {
                var accessor = new UserAccessor(ctx);
                fetched = await accessor.GetbyEmailAsync("frank@example.com");
            }

            // assert
            Assert.IsNotNull(fetched);
            Assert.AreEqual(user.UserId, fetched.UserId);
        }
    }
}
