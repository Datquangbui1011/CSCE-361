using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Respository;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store_service_host.Server.Repositories;

interface ICategoryRepository
{
    // Task<IEnumerable<Category>> GetAsync();
    // Task<IEnumerable<Category>> GetAllAsync();
    // Task<IEnumerable<Category>> GetByIdAsync(Guid id);
    // Task<IEnumerable<Category>> AddAsync(Product product);
    // Task<IEnumerable<Category>> Delete();
    // Task<IEnumerable<Category>> SaveAsync(Product product);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(Guid id);
    Task AddAsync(Category category);
    void Delete(Category category);
    Task SaveAsync();
    
}

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
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
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}