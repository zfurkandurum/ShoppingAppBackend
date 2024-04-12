using ShoppingAppBackend.Data;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Repository;

public class UserRepository : IUserRepository
{
    private DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<ApplicationUser> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public ApplicationUser GetUser(string email)
    {
        return _context.Users.Where(u => u.Email == email).FirstOrDefault();
    }
    
    public bool UserExists(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }

    public bool UpdateUser(ApplicationUser user)
    {
        _context.Update(user);
        return Save();
    }

    public bool DeleteUser(ApplicationUser user)
    {
        _context.Remove(user);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}