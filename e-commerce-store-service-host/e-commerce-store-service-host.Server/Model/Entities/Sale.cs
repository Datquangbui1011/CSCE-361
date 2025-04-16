namespace e_commerce_store_service_host.Server.Model.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Sale
{
    [Key]
    public Guid SaleId { get; set; }
    
    string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    string DiscountType { get; set; }
    
    decimal Discount { get; set; }
    
}