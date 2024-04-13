using Microsoft.AspNetCore.Identity;

namespace ShoppingAppBackend.Models;

public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    
    public List<Cart> Carts { get; set; }
    public List<Order> Orders { get; set; }
    
    public ApplicationUser()
    {
        Carts = new List<Cart> { new Cart { Items = new List<CartItem>() } };
    }
    
}