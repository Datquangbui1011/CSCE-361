using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_store_service_host.Server.Model.Entities;

public class Product
{
    [Key]
    public Guid ProductId { get; set; }
    
    string Name { get; set; }
    
    string? Description { get; set; }
    
    decimal? Price { get; set; }
    
    string? SKU { get; set; }
    
    float? Rating { get; set; }
    
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}