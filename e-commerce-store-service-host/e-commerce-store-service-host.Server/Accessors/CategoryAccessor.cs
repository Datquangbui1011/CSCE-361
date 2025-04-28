using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store_service_host.Server.Accessors;

public interface ICategoryAccessor
{
    // Basic CRUD-like actions
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(Guid id);
    Task AddAsync(Category category);
    void Delete(Category category);
}

public class CategoryAccessor : ICategoryAccessor
{
    private readonly AppDbContext _context;

    public CategoryAccessor(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async void Delete(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
