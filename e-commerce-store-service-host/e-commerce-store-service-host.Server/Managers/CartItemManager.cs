using e_commerce_store_service_host.Server.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;



namespace e_commerce_store_service_host.Server.Services;
public class CartItemManager
{
    private readonly ICartItemAccessor _cartItemRepository;

    public CartItemManager(ICartItemAccessor cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<IEnumerable<CartItem>> GetAllCartItemsAsync()
    {
        return await _cartItemRepository.GetAllAsync();
    }

    public async Task<CartItem> GetCartItemByIdAsync(Guid id)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(id);
        return cartItem;
    }

    public async Task AddCartItemAsync(CartItem cartItem)
    {
        await _cartItemRepository.AddAsync(cartItem);
    }

    public async Task DeleteCartItemAsync(Guid id)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(id);
        if (cartItem != null)
        {
            _cartItemRepository.Delete(cartItem);
        }
    }

}