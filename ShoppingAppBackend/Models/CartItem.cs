namespace ShoppingAppBackend.Models;

public class CartItem
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    public Product Product { get; set; }

}