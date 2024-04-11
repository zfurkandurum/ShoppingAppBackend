using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Interfaces;

public interface IUserRepository
{
    ICollection<ApplicationUser> GetAllUsers();
    ApplicationUser GetUser(int userId);
    bool UserExists(int userId);
    bool CreateUser(ApplicationUser user);
    bool UpdateUser(ApplicationUser user);
    bool DeleteUser(ApplicationUser user);
    bool Save();

}