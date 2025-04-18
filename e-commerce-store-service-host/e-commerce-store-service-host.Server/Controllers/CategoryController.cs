using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using Microsoft.AspNetCore.Http.HttpResults;

namespace e_commerce_store_service_host.Server.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryManager _categoryManager;

    public CategoryController(CategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }


    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryManager.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var category = await _categoryManager.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _categoryManager.AddCategoryAsync(category);
        
        return CreatedAtAction(nameof(GetCategory), new {id = category.CategoryId }, category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _categoryManager.DeleteCategoryAsync(id);
        return NoContent();
    }
}