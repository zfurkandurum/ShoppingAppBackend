namespace ShoppingAppBackend.Dto;

public class ProductDto
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public string Detail { get; set; }
    public string Quantity { get; set; }
    public double Price { get; set; }
    public string ProductPhotoUrl { get; set; }
}