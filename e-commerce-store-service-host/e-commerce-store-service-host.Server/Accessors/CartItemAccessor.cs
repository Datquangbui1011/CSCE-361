using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store_service_host.Server.Accessors
{
    public interface ICartItemAccessor
    {
        Task<IEnumerable<CartItem>> GetAllAsync();
        Task<CartItem> GetByIdAsync(Guid id);
        Task AddAsync(CartItem cartItem);
        void Delete(CartItem cartItem);
    }

    public class CartItemAccessor : ICartItemAccessor
    {
        private readonly AppDbContext _context;

        public CartItemAccessor(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetByIdAsync(Guid id)
        {
            return await _context.CartItems.FindAsync(id);
        }

        public async Task AddAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async void Delete(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}