using backend.Model;

namespace backend.Service;

public interface IAuthService
{
    public User Login(string userName, string password);
    public User FindByUsername(string username);
}