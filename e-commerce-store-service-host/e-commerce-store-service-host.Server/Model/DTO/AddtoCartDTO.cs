namespace e_commerce_store_service_host.Server.Model.DTO;
using System.ComponentModel.DataAnnotations;

public class AddtoCartDTO
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")] 
    public int Quantity { get; set; }
}