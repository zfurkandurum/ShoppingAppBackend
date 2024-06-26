using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Interfaces;

public interface IUserRepository
{
    ICollection<ApplicationUser> GetAllUsers();
    ApplicationUser GetUserById(int id);
    ApplicationUser GetUserByEmail(string email);
    bool UserExists(string email);
    bool UpdateUser(ApplicationUser user);
    bool DeleteUser(ApplicationUser user);
    bool Save();

}