using e_commerce_store_service_host.Server.Model;
using e_commerce_store_service_host.Server.Model.Entities;
//using e_commerce_store_service_host.Server.Respository;
using Microsoft.EntityFrameworkCore;
namespace e_commerce_store_service_host.Server.Accessors;

public interface ISaleAccessor
{
    // Basic CRUD-like actions
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale> GetByIdAsync(Guid id);
    Task AddAsync(Sale sale);
    void Delete(Sale sale);
}

public class SaleAccessor : ISaleAccessor
{
    private readonly AppDbContext _context;

    public SaleAccessor(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales.ToListAsync();
    }

    public async Task<Sale> GetByIdAsync(Guid id)
    {
        return await _context.Sales.FindAsync(id);
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async void Delete(Sale sale)
    {
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
    }
}