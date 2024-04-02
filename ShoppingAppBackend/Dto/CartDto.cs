namespace ShoppingAppBackend.Dto;

public class CartDto
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public double Total { get; set; }
}