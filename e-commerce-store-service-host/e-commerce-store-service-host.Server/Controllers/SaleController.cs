using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;

namespace e_commerce_store_service_host.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly SaleManager _saleManager;

        public SaleController(SaleManager saleManager)
        {
            _saleManager = saleManager;
        }

        [HttpGet]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(Guid id)
        {
            var sale = await _saleManager.GetSaleByIdAsync(id);
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _saleManager.AddSaleAsync(sale);
            return CreatedAtAction(nameof(GetSale), new { id = sale.SaleId }, sale);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            await _saleManager.DeleteSaleAsync(id);
            return NoContent();
        }
    }
}