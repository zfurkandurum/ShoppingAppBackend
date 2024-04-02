namespace ShoppingAppBackend.Models;

public class User
{
    public int UserId { get; set; }
    public int CartId { get; set; }
    public int UserDetailId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserDetail Detail { get; set; }
    public Cart Cart { get; set; }
    public List<Order> Orders { get; set; }
}