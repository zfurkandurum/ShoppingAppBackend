namespace ShoppingAppBackend.Dto;

public class UserDetailDto
{
    public int UserDetailId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}