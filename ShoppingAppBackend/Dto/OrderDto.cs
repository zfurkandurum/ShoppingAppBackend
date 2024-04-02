namespace ShoppingAppBackend.Dto;

public class OrderDto
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public float TotalPrice { get; set; }
}