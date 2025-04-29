using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;

namespace e_commerce_store_service_host.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductManager _productManager;

        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productManager.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productManager.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productManager.DeleteProductAsync(id);
            return NoContent();
        }
    }
}