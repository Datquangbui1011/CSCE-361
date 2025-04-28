using e_commerce_store_service_host.Server.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce_store_service_host.Server.Accessors;
using e_commerce_store_service_host.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;



namespace e_commerce_store_service_host.Server.Services;
public class SaleManager
{
    private readonly SaleAccessor _saleRepository;

    public SaleManager(SaleAccessor saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<Sale>> GetAllSalesAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale> GetSaleByIdAsync(Guid id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        return sale;
    }

    public async Task AddSaleAsync(Sale sale)
    {
        await _saleRepository.AddAsync(sale);
    }

    public async Task DeleteSaleAsync(Guid id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale != null)
        {
            _saleRepository.Delete(sale);
        }
    }

}