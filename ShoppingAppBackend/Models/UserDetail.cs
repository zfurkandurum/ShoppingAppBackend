namespace ShoppingAppBackend.Models;

public class UserDetail
{
    public int UserDetailId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public User User { get; set; }
}