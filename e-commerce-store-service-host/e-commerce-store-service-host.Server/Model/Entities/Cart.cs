using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace e_commerce_store_service_host.Server.Model.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Cart
{
    [Key]
    public Guid CartId { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User User{ get; set; }
    
}