namespace ShoppingAppBackend.Dto;

public class UserDto
{
    public int UserId { get; set; }
    public int CartId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}