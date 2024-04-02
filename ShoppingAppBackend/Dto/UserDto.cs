namespace ShoppingAppBackend.Dto;

public class UserDto
{
    public int UserId { get; set; }
    public int CartId { get; set; }
    public int UserDetailId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}