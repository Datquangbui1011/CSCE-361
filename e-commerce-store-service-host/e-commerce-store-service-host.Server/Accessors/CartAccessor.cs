using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store_service_host.Server.Accessors;

public interface ICartAccessor
{
    // Basic CRUD-like actions
    Task<IEnumerable<Cart>> GetAllAsync();
    Task<Cart> GetByIdAsync(Guid id);
    Task AddAsync(Cart cart);
    void Delete(Cart cart);
    Task SaveAsync();
   
}

public class CartAccessor: ICartAccessor
{
    private readonly AppDbContext _context;

    public CartAccessor(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await _context.Carts.ToListAsync();
    }

    public async Task<Cart> GetByIdAsync(Guid id)
    {
        return await _context.Carts.FindAsync(id);
    }

    public async Task AddAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
    }

    public void Delete(Cart cart)
    {
        _context.Carts.Remove(cart);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}