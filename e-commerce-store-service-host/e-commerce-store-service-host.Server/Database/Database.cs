using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Address { get; set; }
    
    public ICollection<PaymentType>? PaymentTypes { get; set; }
    public ICollection<Cart>?Carts { get; set; }
}

public class PaymentType
{
    [Key]
    public int PaymentTypeId { get; set; }
    public string? CardType { get; set; }
    public string? CardNumber { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
}

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductImage { get; set; }
    public string? ProductManufacturer { get; set; }
    public string? ProductDimension { get; set; }
    public string? ProductCategory { get; set; }
    [Range(1,5, ErrorMessage = "Product rating must be between 1 and 5")]
    public int ProductRating { get; set; }
    public string? ProductSKU { get; set; }
    public int Lbs { get; set; }
}

public class Cart
{
    [Key]
    public int CartId { get; set; }
    public int CartSize { get; set; }
    public DateTime AddedAt { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}

public class Sale
{
    [Key]
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime EndTime { get; set; }
    public string? SaleProcess {get;set;}
    
    public int CartId { get; set; }
    public Cart? Cart { get; set; }
    
}