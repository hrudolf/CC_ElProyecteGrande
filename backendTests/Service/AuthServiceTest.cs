using backend.Model;
using backend.Service;

namespace backendTests.Service;

public class AuthServiceTest
{
    [Fact]
    public void CorrectLogin()
    {
        var dbContext = Context.GetDbContext();
        var authService = new AuthService(dbContext);
        var userName = "admin";
        var password = "admin";

        User returnedUser = authService.Login(userName, password);
        Assert.Equal(userName, returnedUser.Username);
        Assert.Equal(UserRole.Admin, returnedUser.Role);
    }
    
    [Fact]
    public void InvalidUserName()
    {
        var dbContext = Context.GetDbContext();
        var authService = new AuthService(dbContext);
        var userName = "admin2";
        var password = "admin";

        var ex = Assert.Throws<Exception>(() => authService.Login(userName, password));
        Assert.Equal("Invalid username", ex.Message);
    }
    
    [Fact]
    public void IncorrectPassword()
    {
        var dbContext = Context.GetDbContext();
        var authService = new AuthService(dbContext);
        var userName = "admin";
        var password = "adm";

        var ex = Assert.Throws<Exception>(() => authService.Login(userName, password));
        Assert.Equal("Incorrect password", ex.Message);
    }
    
    [Fact]
    public void FindByUsername_ValidUserName()
    {
        var dbContext = Context.GetDbContext();
        var authService = new AuthService(dbContext);
        var userName = "admin";

        User returnedUser = authService.FindByUsername(userName);
        
        Assert.Equal(userName, returnedUser.Username);
        Assert.Equal(UserRole.Admin, returnedUser.Role);
    }
    
    [Fact]
    public void FindByUsername_InvalidUserName()
    {
        var dbContext = Context.GetDbContext();
        var authService = new AuthService(dbContext);
        var userName = "admin2";

        var ex = Assert.Throws<Exception>(() => authService.FindByUsername(userName));
        Assert.Equal("Invalid username", ex.Message);
    }
}