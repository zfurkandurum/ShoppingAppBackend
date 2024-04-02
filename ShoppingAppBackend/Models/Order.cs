namespace ShoppingAppBackend.Models;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public float TotalPrice { get; set; }
    public User User { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}