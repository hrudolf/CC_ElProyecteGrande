using backend.Database;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Service;

public class AuthService : IAuthService
{
    private readonly DataContext _context;

    public AuthService(DataContext context)
    {
        _context = context;
    }
    public User Login(string userName, string password)
    {
        User? userInDb = _context.Users
            .Include(user => user.Employee)
            .FirstOrDefault(user => user.Username == userName);
        if (userInDb == null) throw new Exception("Invalid username");
        if (userInDb.Password != password) throw new Exception("Incorrect password");
        return userInDb;
    }

    public User FindByUsername(string username)
    {
        User? userInDb = _context.Users
            .Include(user => user.Employee)
            .FirstOrDefault(user => user.Username == username);
        if (userInDb == null) throw new Exception("Invalid username");
        return userInDb;
    }
}