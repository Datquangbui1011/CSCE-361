using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;

namespace e_commerce_store_service_host.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly CartItemManager _cartItemManager;

        public CartItemController(CartItemManager cartItemManager)
        {
            _cartItemManager = cartItemManager;
        }

        [HttpGet]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItem(Guid id)
        {
            var cartItem = await _cartItemManager.GetCartItemByIdAsync(id);
            if (cartItem == null)
                return NotFound();
            return Ok(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItem cartItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cartItemManager.AddCartItemAsync(cartItem);
            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.CartItemId }, cartItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(Guid id)
        {
            await _cartItemManager.DeleteCartItemAsync(id);
            return NoContent();
        }
    }
}