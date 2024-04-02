namespace ShoppingAppBackend.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; }
    public List<Product> Products { get; set; }

}