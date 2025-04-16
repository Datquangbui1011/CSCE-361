using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace e_commerce_store_service_host.Server.Model.Entities;
public class Category
{
    public ICollection<Product> Products { get; set; } // If we need to return the collection of all the products (e.g drop down menu)
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}