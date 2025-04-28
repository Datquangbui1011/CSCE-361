using System.ComponentModel.DataAnnotations;

namespace e_commerce_store_service_host.Server.Model.Entities;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Address { get; set; }
}