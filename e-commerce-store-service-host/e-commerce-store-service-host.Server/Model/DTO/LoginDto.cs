using System.ComponentModel.DataAnnotations;

namespace e_commerce_store_service_host.Server.Model.DTO;

public class LoginDto
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}