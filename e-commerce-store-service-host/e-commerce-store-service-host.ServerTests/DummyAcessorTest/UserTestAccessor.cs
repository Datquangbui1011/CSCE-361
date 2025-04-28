using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store_service_host.ServerTests.Accessor;

[TestClass]
public class UserRepositoryTests
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
    public async Task GetAllAsync_WhenUsersExist_ReturnsAllUsers()
    {
        // Arrange
        var user1 = new User { UserId = Guid.NewGuid(), Name = "Alice", Email = "alice@example.com", Password = "pw1", Address = "123 A St" };
        var user2 = new User { UserId = Guid.NewGuid(), Name = "Bob", Email = "bob@example.com", Password = "pw2", Address = "456 B Ave" };
        await using (var context = new AppDbContext(_dbOptions))
        {
            context.Users.AddRange(user1, user2);
            await context.SaveChangesAsync();
        }

        await using (var context = new AppDbContext(_dbOptions))
        {
            var repo = new User_Accessor(context);

            // Act
            var all = (await repo.GetAllAsync()).ToList();

            // Assert
            Assert.AreEqual(2, all.Count, "Should return exactly two users");
            CollectionAssert.AreEquivalent(
                new[] { user1.UserId, user2.UserId },
                all.Select(u => u.UserId).ToArray());
        }
    }

    [TestMethod]
    public async Task GetByIdAsync_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var expected = new User { UserId = Guid.NewGuid(), Name = "Carol", Email = "carol@example.com", Password = "pw3", Address = "789 C Blvd" };
        await using (var context = new AppDbContext(_dbOptions))
        {
            context.Users.Add(expected);
            await context.SaveChangesAsync();
        }

        await using (var context = new AppDbContext(_dbOptions))
        {
            var repo = new User_Accessor(context);

            // Act
            var actual = await repo.GetByIdAsync(expected.UserId);

            // Assert
            Assert.IsNotNull(actual, "Should find the user by ID");
            Assert.AreEqual(expected.Email, actual.Email, "Email should match");
            Assert.AreEqual(expected.Name, actual.Name, "Name should match");
        }
    }

    [TestMethod]
    public async Task AddAsync_WhenCalled_AddsUserAndPersists()
    {
        // Arrange
        var toAdd = new User { UserId = Guid.NewGuid(), Name = "Dave", Email = "dave@example.com", Password = "pw4", Address = "101 D Rd" };

        await using (var context = new AppDbContext(_dbOptions))
        {
            var repo = new User_Accessor(context);

            // Act
            await repo.AddAsync(toAdd);
        }

        // Assert by using a fresh context
        await using (var context = new AppDbContext(_dbOptions))
        {
            var persisted = await context.Users.FindAsync(toAdd.UserId);
            Assert.IsNotNull(persisted, "User should have been persisted");
            Assert.AreEqual("Dave", persisted.Name);
        }
    }
}

