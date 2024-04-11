using Microsoft.AspNetCore.Identity;

namespace ShoppingAppBackend.Models;

public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    public List<Order> Orders { get; set; }
}