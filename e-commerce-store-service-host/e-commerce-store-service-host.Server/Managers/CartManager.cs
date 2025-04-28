using e_commerce_store_service_host.Server.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;


namespace e_commerce_store_service_host.Server.Services;
public class CartManager
{
    private readonly CartAccessor _cartRepository;

    public CartManager(CartAccessor cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<IEnumerable<Cart>> GetAllCartsAsync()
    {
        return await _cartRepository.GetAllAsync();
    }

    public async Task<Cart> GetCartByIdAsync(Guid id)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        return cart;
    }

    public async Task AddCartAsync(Cart cart)
    {
        await _cartRepository.AddAsync(cart);
    }

    public async Task DeleteCartAsync(Guid id)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        if (cart != null)
        {
            _cartRepository.Delete(cart);
        }
    }

}