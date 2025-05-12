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
    public class SaleRepositoryTests
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
        public async Task GetAllAsync_WhenSalesExist_ReturnsAllSales()
        {
            // Arrange
            var s1 = new Sale
            {
                SaleId = Guid.NewGuid(),
                Name = "Spring Sale",
                StartDate = new DateTime(2025, 4, 1),
                EndDate = new DateTime(2025, 4, 15),
                DiscountType = "Percentage",
                Discount = 0.10m
            };
            var s2 = new Sale
            {
                SaleId = Guid.NewGuid(),
                Name = "Summer Sale",
                StartDate = new DateTime(2025, 6, 1),
                EndDate = new DateTime(2025, 6, 30),
                DiscountType = "Flat",
                Discount = 5.00m
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Sales.AddRange(s1, s2);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new SaleAccessor(context);
                var all = (await repo.GetAllAsync()).ToList();

                Assert.AreEqual(2, all.Count, "Should return exactly two sales");
                CollectionAssert.AreEquivalent(
                    new[] { s1.SaleId, s2.SaleId },
                    all.Select(s => s.SaleId).ToArray());
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_WhenSaleExists_ReturnsSale()
        {
            // Arrange
            var expected = new Sale
            {
                SaleId = Guid.NewGuid(),
                Name = "Holiday Sale",
                StartDate = new DateTime(2025, 12, 1),
                EndDate = new DateTime(2025, 12, 31),
                DiscountType = "Percentage",
                Discount = 0.25m
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                context.Sales.Add(expected);
                await context.SaveChangesAsync();
            }

            // Act & Assert
            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new SaleAccessor(context);
                var actual = await repo.GetByIdAsync(expected.SaleId);

                Assert.IsNotNull(actual, "Should find the sale by ID");
                Assert.AreEqual(expected.Name, actual.Name, "Name should match");
                Assert.AreEqual(expected.StartDate, actual.StartDate, "StartDate should match");
                Assert.AreEqual(expected.EndDate, actual.EndDate, "EndDate should match");
                Assert.AreEqual(expected.DiscountType, actual.DiscountType, "DiscountType should match");
                Assert.AreEqual(expected.Discount, actual.Discount, "Discount value should match");
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenCalled_AddsSaleAndPersists()
        {
            // Arrange
            var toAdd = new Sale
            {
                SaleId = Guid.NewGuid(),
                Name = "Clearance",
                StartDate = new DateTime(2025, 7, 1),
                EndDate = new DateTime(2025, 7, 7),
                DiscountType = "Flat",
                Discount = 10.00m
            };

            await using (var context = new AppDbContext(_dbOptions))
            {
                var repo = new SaleAccessor(context);
                await repo.AddAsync(toAdd);
            }

            // Assert by using a fresh context
            await using (var context = new AppDbContext(_dbOptions))
            {
                var persisted = await context.Sales.FindAsync(toAdd.SaleId);
                Assert.IsNotNull(persisted, "Sale should have been persisted");
                Assert.AreEqual(toAdd.Name, persisted.Name, "Name should match");
                Assert.AreEqual(toAdd.StartDate, persisted.StartDate, "StartDate should match");
                Assert.AreEqual(toAdd.EndDate, persisted.EndDate, "EndDate should match");
                Assert.AreEqual(toAdd.DiscountType, persisted.DiscountType, "DiscountType should match");
                Assert.AreEqual(toAdd.Discount, persisted.Discount, "Discount value should match");
            }
        }
    }
}
