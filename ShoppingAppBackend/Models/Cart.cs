namespace ShoppingAppBackend.Models;

public class Cart
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public float Total { get; set; }
    public ApplicationUser User { get; set; }
    public List<Product> Products { get; set; }
    public List<CartItem> Items { get; set; }
}