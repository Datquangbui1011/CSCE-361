using e_commerce_store_service_host.Server.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;



namespace e_commerce_store_service_host.Server.Services;
public class ProductManager
{
    private readonly ProductAccessor _productRepository;

    public ProductManager(ProductAccessor productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllManagersAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product;
    }

    public async Task AddProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            _productRepository.Delete(product);
        }
    }

}