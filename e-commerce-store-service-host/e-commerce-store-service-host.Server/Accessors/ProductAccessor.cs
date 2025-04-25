using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
//using e_commerce_store_service_host.Server.Respository;
using Microsoft.EntityFrameworkCore;
namespace e_commerce_store_service_host.Server.Accessors;

public interface IProductAccessor
{
    // Basic CRUD-like actions
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
    void Delete(Product product);
    Task SaveAsync();
   
}

public class ProductAccessor : IProductAccessor
{
    private readonly AppDbContext _context;

    public ProductAccessor(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}