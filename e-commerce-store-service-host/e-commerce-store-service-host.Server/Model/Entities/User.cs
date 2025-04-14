namespace e_commerce_store_service_host.Server.Model.Entities;

public class User
{
    int UserId { get; set; }
    string Name { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    string Address { get; set; }
}