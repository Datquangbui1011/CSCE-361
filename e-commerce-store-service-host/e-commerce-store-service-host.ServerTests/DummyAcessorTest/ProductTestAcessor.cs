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
    public class ProductRepositoryTests
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
        public async Task GetAllAsync_WhenProductsExist_ReturnsAllProducts()
        {
            // Arrange
            var category = new Category
            {
                CategoryId  = Guid.NewGuid(),
                Name        = "Electronics",
                Description = "All electronic items for sale"
            };
            var p1 = new Product
            {
                ProductId   = Guid.NewGuid(),
                Name        = "Widget A",
                Description = "First widget",
                Price       = 9.99m,
                SKU         = "WIDGET-A",
                Rating      = 4.2f,
                Category    = category
            };
            var p2 = new Product
            {
                ProductId   = Guid.NewGuid(),
                Name        = "Widget B",
                Description = "Second widget",
                Price       = 19.99m,
                SKU         = "WIDGET-B",
                Rating      = 3.8f,
                Category    = category
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Categories.Add(category);
                context.Products.AddRange(p1, p2);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new ProductAccessor(context);
                var all = (await repo.GetAllAsync()).ToList();

                Assert.AreEqual(2, all.Count, "Should return exactly two products");
                CollectionAssert.AreEquivalent(
                    new[] { p1.ProductId, p2.ProductId },
                    all.Select(p => p.ProductId).ToArray());
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            var category = new Category
            {
                CategoryId  = Guid.NewGuid(),
                Name        = "Books",
                Description = "Printed reading materials"
            };
            var expected = new Product
            {
                ProductId   = Guid.NewGuid(),
                Name        = "C# in Depth",
                Description = "Programming book",
                Price       = 39.95m,
                SKU         = "BOOK-CSHARP",
                Rating      = 4.9f,
                Category    = category
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Categories.Add(category);
                context.Products.Add(expected);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new ProductAccessor(context);
                var actual = await repo.GetByIdAsync(expected.ProductId);

                Assert.IsNotNull(actual, "Should find the product by ID");
                Assert.AreEqual(expected.Name, actual.Name, "Name should match");
                Assert.AreEqual(expected.Price, actual.Price, "Price should match");
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenCalled_AddsProductAndPersists()
        {
            // Arrange
            var category = new Category
            {
                CategoryId  = Guid.NewGuid(),
                Name        = "Gadgets",
                Description = "Small electronic gadgets"
            };
            var toAdd = new Product
            {
                ProductId   = Guid.NewGuid(),
                Name        = "Gizmo X",
                Description = "Latest gadget",
                Price       = 49.99m,
                SKU         = "GIZMO-X",
                Rating      = 4.5f,
                Category    = category
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                // seed the category first
                context.Categories.Add(category);
                await context.SaveChangesAsync();

                var repo = new ProductAccessor(context);
                await repo.AddAsync(toAdd);
            }

            // Assert by using a fresh context
            await using (var context = new AppDbContext(_dbOptions))
            {
                var persisted = await context.Products.FindAsync(toAdd.ProductId);
                Assert.IsNotNull(persisted, "Product should have been persisted");
                Assert.AreEqual("Gizmo X", persisted.Name);
                Assert.AreEqual(49.99m, persisted.Price);
            }
        }
    }
}
