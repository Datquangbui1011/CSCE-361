using e_commerce_store_service_host.Server.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;



namespace e_commerce_store_service_host.Server.Services;
public class CategoryManager
{
    private readonly ICategoryAccessor _categoryRepository;

    public CategoryManager(ICategoryAccessor categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _categoryRepository.AddAsync(category);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            _categoryRepository.Delete(category);
        }
    }

}