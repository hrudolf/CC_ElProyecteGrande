using backend.Database;
using backend.Model;

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
        User? userInDb = _context.Users.FirstOrDefault(user => user.LoginName == userName);
        if (userInDb == null) throw new Exception("Invalid username");
        if (userInDb.Password != password) throw new Exception("Incorrect password");
        return userInDb;
    }
}