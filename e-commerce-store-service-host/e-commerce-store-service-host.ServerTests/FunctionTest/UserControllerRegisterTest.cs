using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Services;
using e_commerce_store_service_host.Server.Controllers;

namespace e_commerce_store_service_host.ServerTests.Controllers
{
    [TestClass]
    public class CategoryTests
    {
    private DbContextOptions<AppDbContext> _dbOptions;
    private AppDbContext _context { get; set; } = null!;
    private CategoryAccessor _accessor = null!;
    private CategoryManager _manager = null!;
    private CategoryController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        // Use a fresh in-memory database for each test
        _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(_dbOptions);
        _accessor = new CategoryAccessor(_context);
        _manager = new CategoryManager(_accessor);
        _controller = new CategoryController(_manager);
    }

    [TestMethod]
    public async Task GetCategories_ReturnsOk_WithAllCategories()
    {
        // Arrange: seed two categories
        var c1 = new Category { CategoryId = Guid.NewGuid(), Name = "Cat A" };
        var c2 = new Category { CategoryId = Guid.NewGuid(), Name = "Cat B" };
        _context.Categories.AddRange(c1, c2);
        await _context.SaveChangesAsync();

        // Act
        IActionResult actionResult = await _controller.GetCategories();

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        var okResult = (OkObjectResult)actionResult;

        // Check that the value is the expected collection type
        Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Category>));
        var list = (IEnumerable<Category>)okResult.Value;
        Assert.IsNotNull(list);
        var fetched = list.ToList();
        Assert.AreEqual(2, fetched.Count);
        CollectionAssert.AreEquivalent(
            new[] { c1.CategoryId, c2.CategoryId },
            fetched.Select(c => c.CategoryId).ToArray()
        );
    }

    [TestMethod]
    public async Task GetCategory_ReturnsOk_WhenExists()
    {
        // Arrange
        var c = new Category { CategoryId = Guid.NewGuid(), Name = "Solo" };
        _context.Categories.Add(c);
        await _context.SaveChangesAsync();

        // Act
        IActionResult actionResult = await _controller.GetCategory(c.CategoryId);

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        var ok = (OkObjectResult)actionResult;
        Assert.AreEqual(c, ok.Value);
    }

    [TestMethod]
    public async Task GetCategory_ReturnsNotFound_WhenMissing()
    {
        // Act
        IActionResult actionResult = await _controller.GetCategory(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task CreateCategory_ReturnsCreatedAtAction_AndPersists()
    {
        // Arrange
        var toAdd = new Category { CategoryId = Guid.NewGuid(), Name = "NewCat" };

        // Act
        IActionResult actionResult = await _controller.CreateCategory(toAdd);

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
        var created = (CreatedAtActionResult)actionResult;
        Assert.AreEqual(nameof(_controller.GetCategory), created.ActionName);
        Assert.AreEqual(toAdd.CategoryId, created.RouteValues["id"]);
        Assert.AreEqual(toAdd, created.Value);

        // persisted?
        var persisted = await _context.Categories.FindAsync(toAdd.CategoryId);
        Assert.IsNotNull(persisted);
        Assert.AreEqual("NewCat", persisted!.Name);
    }

    [TestMethod]
    public async Task DeleteCategory_ReturnsNoContent_AndRemoves()
    {
        // Arrange
        var keep = new Category { CategoryId = Guid.NewGuid(), Name = "Keep" };
        var remove = new Category { CategoryId = Guid.NewGuid(), Name = "Remove" };
        _context.Categories.AddRange(keep, remove);
        await _context.SaveChangesAsync();

        // Act
        IActionResult actionResult = await _controller.DeleteCategory(remove.CategoryId);

        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        var remaining = _context.Categories.ToList();
        Assert.AreEqual(1, remaining.Count);
        Assert.AreEqual(keep.CategoryId, remaining[0].CategoryId);
    }
    }
}
