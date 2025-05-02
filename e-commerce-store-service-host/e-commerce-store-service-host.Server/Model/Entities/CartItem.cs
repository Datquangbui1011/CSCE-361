using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace e_commerce_store_service_host.Server.Model.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public Guid CartItemId { get; set; }

    public int Quantity { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    [Required]
    public Guid CartId { get; set; }
    
    [ForeignKey("CartId")]
    public virtual Cart Cart { get; set; }
    
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}