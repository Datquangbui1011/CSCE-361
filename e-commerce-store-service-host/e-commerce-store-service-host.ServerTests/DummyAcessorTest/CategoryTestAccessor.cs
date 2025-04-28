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
    public class CategoryRepositoryTests
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
        public async Task GetAllAsync_WhenCategoriesExist_ReturnsAllCategories()
        {
            // Arrange
            var c1 = new Category
            {
                CategoryId  = Guid.NewGuid(),
                Name        = "Electronics",
                Description = "Devices and gadgets"
            };
            var c2 = new Category
            {
                CategoryId  = Guid.NewGuid(),
                Name        = "Books",
                Description = "Printed and digital books"
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Categories.AddRange(c1, c2);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new CategoryAccessor(context);
                var all  = (await repo.GetAllAsync()).ToList();

                Assert.AreEqual(2, all.Count, "Should return exactly two categories");
                CollectionAssert.AreEquivalent(
                    new[] { c1.CategoryId, c2.CategoryId },
                    all.Select(c => c.CategoryId).ToArray());
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_WhenCategoryExists_ReturnsCategory()
        {
            // Arrange
            var expected = new Category
            {
                CategoryId  = Guid.NewGuid(),
                Name        = "Gadgets",
                Description = "Small electronic gadgets"
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Categories.Add(expected);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo   = new CategoryAccessor(context);
                var actual = await repo.GetByIdAsync(expected.CategoryId);

                Assert.IsNotNull(actual, "Should find the category by ID");
                Assert.AreEqual(expected.Name, actual.Name, "Name should match");
                Assert.AreEqual(expected.Description, actual.Description, "Description should match");
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenCalled_AddsCategoryAndPersists()
        {
            // Arrange
            var toAdd = new Category
            {
                CategoryId  = Guid.NewGuid(),
                Name        = "Clothing",
                Description = "Apparel and accessories"
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new CategoryAccessor(context);
                await repo.AddAsync(toAdd);
            }

            // Assert by using a fresh context
            await using (var context = new AppDbContext(_dbOptions))
            {
                var persisted = await context.Categories.FindAsync(toAdd.CategoryId);
                Assert.IsNotNull(persisted, "Category should have been persisted");
                Assert.AreEqual(toAdd.Name, persisted.Name, "Name should match");
                Assert.AreEqual(toAdd.Description, persisted.Description, "Description should match");
            }
        }
    }
}
