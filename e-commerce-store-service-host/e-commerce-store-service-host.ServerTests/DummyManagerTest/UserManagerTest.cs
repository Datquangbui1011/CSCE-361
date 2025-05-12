using System;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace e_commerce_store_service_host.ServerTests.Services
{
    [TestClass]
    public class UserManagerTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;
        private AppDbContext _context = null!;
        private UserAccessor _accessor = null!;
        private UserManager _manager = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            // each test gets a fresh in-memory database
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(_dbOptions);
            _accessor = new UserAccessor(_context);
            _manager = new UserManager(_accessor);
        }

        [TestMethod]
        public async Task GetAllUsersAsync_WhenUsersExist_ReturnsAllUsers()
        {
            // Arrange
            var u1 = new User { UserId = Guid.NewGuid(), Name = "Alice", Email = "alice@x.com", Password = "p1", Address = "Addr1" };
            var u2 = new User { UserId = Guid.NewGuid(), Name = "Bob",   Email = "bob@x.com",   Password = "p2", Address = "Addr2" };
            _context.Users.AddRange(u1, u2);
            await _context.SaveChangesAsync();

            // Act
            var list = (await _manager.GetAllUsersAsync()).ToList();

            // Assert
            Assert.AreEqual(2, list.Count, "Should return exactly two users");
            CollectionAssert.AreEquivalent(
                new[] { u1.UserId, u2.UserId },
                list.Select(u => u.UserId).ToArray());
        }

        [TestMethod]
        public async Task GetUserByIdAsync_WhenUserExists_ReturnsThatUser()
        {
            // Arrange
            var expected = new User { UserId = Guid.NewGuid(), Name = "Carol", Email = "carol@x.com", Password = "p3", Address = "Addr3" };
            _context.Users.Add(expected);
            await _context.SaveChangesAsync();

            // Act
            var actual = await _manager.GetUserByIdAsync(expected.UserId);

            // Assert
            Assert.IsNotNull(actual, "User should be found");
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_WhenUserDoesNotExist_ReturnsNull()
        {
            // Act
            var actual = await _manager.GetUserByIdAsync(Guid.NewGuid());

            // Assert
            Assert.IsNull(actual, "Should return null for missing user");
        }

        [TestMethod]
        public async Task AddUserAsync_WhenCalled_ReturnsTrueAndPersists()
        {
            // Arrange
            var toAdd = new User { UserId = Guid.NewGuid(), Name = "Dave", Email = "dave@x.com", Password = "p4", Address = "Addr4" };

            // Act
            var result = await _manager.AddUserAsync(toAdd);

            // Assert
            Assert.IsTrue(result, "Should return true on successful add");
            var persisted = await _context.Users.FindAsync(toAdd.UserId);
            Assert.IsNotNull(persisted, "User should be in database");
            Assert.AreEqual("Dave", persisted!.Name);
        }

        [TestMethod]
        public async Task DeleteUserAsync_WhenUserExists_RemovesUser()
        {
            // Arrange
            var keep = new User { UserId = Guid.NewGuid(), Name = "Keep", Email = "keep@x.com", Password = "p5", Address = "Addr5" };
            var remove = new User { UserId = Guid.NewGuid(), Name = "Remove", Email = "rem@x.com", Password = "p6", Address = "Addr6" };
            _context.Users.AddRange(keep, remove);
            await _context.SaveChangesAsync();

            // Act
            await _manager.DeleteUserAsync(remove.UserId);

            // Assert
            var all = await _context.Users.ToListAsync();
            Assert.AreEqual(1, all.Count, "One user should remain");
            Assert.AreEqual(keep.UserId, all[0].UserId);
        }

        [TestMethod]
        public async Task GetUserByEmailAsync_WhenUserExists_ReturnsThatUser()
        {
            // Arrange
            var expected = new User { UserId = Guid.NewGuid(), Name = "EmailTest", Email = "test@x.com", Password = "p7", Address = "Addr7" };
            _context.Users.Add(expected);
            await _context.SaveChangesAsync();

            // Act
            var actual = await _manager.GetUserByEmailAsync("TEST@x.com"); // case-insensitive

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.UserId, actual.UserId);
        }
    }
}
