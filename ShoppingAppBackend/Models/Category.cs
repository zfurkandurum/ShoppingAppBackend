namespace ShoppingAppBackend.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryPhotoUrl { get; set; }
    public List<Product> Products { get; set; }
}