namespace ShoppingAppBackend.Dto;

public class CartItemDto
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
}