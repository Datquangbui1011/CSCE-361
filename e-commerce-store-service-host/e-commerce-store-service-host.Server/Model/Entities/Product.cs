using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_store_service_host.Server.Model.Entities;

public class Product
{
    [Key]
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? SKU { get; set; }

    public float? Rating { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}