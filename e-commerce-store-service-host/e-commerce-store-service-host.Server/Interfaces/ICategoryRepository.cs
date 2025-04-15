using e_commerce_store_service_host.Server.Model.Entities;

namespace e_commerce_store_service_host.Server.Interfaces;
public interface ICategoryRepository
{
    // Basic CRUD-like actions
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(int id);
    Task AddAsync(Category category);
    void Delete(Category category);
    Task SaveAsync();
}
