using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using Microsoft.AspNetCore.Http.HttpResults;

namespace e_commerce_store_service_host.Server.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly CartManager _CartManager;

    public CartController(CartManager CartManager)
    {
        _CartManager = CartManager;
    }


    [HttpGet]
    public async Task<IActionResult> GetCarts()
    {
        var carts = await _CartManager.GetAllCartsAsync();
        return Ok(carts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCart(Guid id)
    {
        var cart = await _CartManager.GetCartByIdAsync(id);
        if (cart == null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] Cart cart)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _CartManager.AddCartAsync(cart);

        return CreatedAtAction(nameof(GetCart), new { id = cart.CartId }, cart);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(Guid id)
    {
        await _CartManager.DeleteCartAsync(id);
        return NoContent();
    }
}