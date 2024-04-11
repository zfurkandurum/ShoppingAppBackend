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

    public ApplicationUser GetUser(int userId)
    {
        return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
    }

    public bool UserExists(int userId)
    {
        return _context.Users.Any(u => u.Id == userId);
    }
    

    public bool CreateUser(ApplicationUser user)
    {
        _context.Add(user);
        return Save();
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