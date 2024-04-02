namespace ShoppingAppBackend.Models;

public class Cart
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public float Total { get; set; }
    public User User { get; set; }
    public List<CartItem> Items { get; set; }
}