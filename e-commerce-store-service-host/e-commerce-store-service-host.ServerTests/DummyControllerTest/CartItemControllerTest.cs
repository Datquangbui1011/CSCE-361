using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Services;
using e_commerce_store_service_host.Server.Controllers;

namespace e_commerce_store_service_host.ServerTests.Controllers
{
    [TestClass]
    public class CartItemControllerInMemoryTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;
        private AppDbContext _context = null!;
        private CartItemAccessor _accessor = null!;
        private CartItemManager _manager = null!;
        private CartItemController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(_dbOptions);
            _accessor = new CartItemAccessor(_context);
            _manager = new CartItemManager(_accessor);
            _controller = new CartItemController(_manager);
        }

        [TestMethod]
        public async Task GetCartItem_ReturnsOk_WhenExists()
        {
            // Arrange
            var item = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 3
            };
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();

            // Act
            IActionResult result = await _controller.GetCartItem(item.CartItemId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = (OkObjectResult)result;
            Assert.AreSame(item, ok.Value);
        }

        [TestMethod]
        public async Task GetCartItem_ReturnsNotFound_WhenMissing()
        {
            // Act
            IActionResult result = await _controller.GetCartItem(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateCartItem_ReturnsCreatedAtAction_AndPersists()
        {
            // Arrange
            var toAdd = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 5
            };

            // Act
            IActionResult result = await _controller.CreateCartItem(toAdd);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var created = (CreatedAtActionResult)result;
            Assert.AreEqual(nameof(_controller.GetCartItem), created.ActionName);
            Assert.AreEqual(toAdd.CartItemId, created.RouteValues["id"]);

            var persisted = await _context.CartItems.FindAsync(toAdd.CartItemId);
            Assert.IsNotNull(persisted);
            Assert.AreEqual(5, persisted.Quantity);
        }

        [TestMethod]
        public async Task CreateCartItem_ReturnsBadRequest_WhenModelInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Quantity", "Required");

            // Act
            IActionResult result = await _controller.CreateCartItem(new CartItem());

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteCartItem_ReturnsNoContent_AndRemoves()
        {
            // Arrange
            var keep = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 1
            };
            var remove = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                CartId = keep.CartId,
                ProductId = Guid.NewGuid(),
                Quantity = 2
            };
            _context.CartItems.AddRange(keep, remove);
            await _context.SaveChangesAsync();

            // Act
            IActionResult result = await _controller.DeleteCartItem(remove.CartItemId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var remaining = _context.CartItems.ToList();
            Assert.AreEqual(1, remaining.Count);
            Assert.AreEqual(keep.CartItemId, remaining[0].CartItemId);
        }
    }
}
