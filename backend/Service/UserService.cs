using backend.Database;
using backend.Model;

namespace backend.Service;

public class UserService : IUserService
{
    
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    public User Create(User item)
    {
        _context.Users.Add(item);
        _context.SaveChanges();
        return item;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User? GetById(int id)
    {
        return _context.Users.FirstOrDefault(shift => shift.Id == id);
    }

    public User? Delete(int id)
    {
        User? userInDb = GetById(id);
        if (userInDb != null)
        {
            //We have to load employees, to allow removal of Shifts (FK restraint)
            var employees = _context.Employees.ToList();
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return userInDb;
        }

        return null;
    }

    public User? Update(User updatedData)
    {
        User? userInDb = GetById(updatedData.Id);
        if (userInDb != null)
        {
            if (updatedData.Username != String.Empty)
            {
                userInDb.Username = updatedData.Username;
            }

            if (updatedData.Password != String.Empty)
            {
                //TODO hash password
                userInDb.Password = updatedData.Password;
            }

            _context.SaveChanges();
            return userInDb;
        }

        return null;
    }
}