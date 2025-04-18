using e_commerce_store_service_host.Server.Model.Entities;
using e_commerce_store_service_host.Server.Respository;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Interfaces;

namespace e_commerce_store_service_host.Server.Services;
public class CategoryManager
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveAsync();
        }
    }
    
}