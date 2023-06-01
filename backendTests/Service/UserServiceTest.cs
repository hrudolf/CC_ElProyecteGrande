using backend.Model;
using backend.Service;

namespace backendTests.Service;

public class UserServiceTest
{
    private Context _context = new Context();
    
    [Fact]
    public void CreateUserReturnsCorrectIdAndBothUserEntitiesAreEqual()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var userName = "TestMaster";
        var password = "password";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        User returnedUser = userService.Create(newUser);
        
        Assert.Equal(newUser, returnedUser);
        Assert.Equal(newUser.Id, returnedUser.Id);
    }
    
    [Fact]
    public void GetAllReturnCorrectNumber()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var expectedCount = 3; //1 user created by seed + 1 admin + 1 accountant

        var userCount = userService.GetAll().Count();
        
        Assert.Equal(expectedCount, userCount);
    }
    
    [Fact]
    public void ReturnsUserWithId()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var userName = "TestMaster";
        var password = "password";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        userService.Create(newUser);
        
        var result = userService.GetById(newUser.Id);
        
        Assert.NotNull(result);
        Assert.Equal(userName, result.Username);
    }
    
    [Fact]
    public void ReturnsNullIdNotFound()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var nonExistingId = 5000; //30 users created by seed + 1 admin

        var result = userService.GetById(nonExistingId);
        
        Assert.Null(result);
    }
    
    [Fact]
    public void DeleteExistingReturnsCorrectData()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var userName = "TestMaster";
        var password = "password";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        userService.Create(newUser);
        
        var result = userService.Delete(newUser.Id);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.Equal(userName, result.Username);
    }
    
    [Fact]
    public void DeleteNonExistingReturnsNull()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var nonExistingId = 5000;
        
        var result = userService.Delete(nonExistingId);
        
        Assert.Null(result);
    }
    
    [Fact]
    public void UpdateNonExistingUserReturnsNull()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var nonExistingUserId = 5000;
        var updatedUser = new User() { Id = nonExistingUserId };
        
        var result = userService.Update(updatedUser);
        
        Assert.Null(result);
    }
    
    [Fact]
    public void UpdateExistingUsersUsername()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var userName = "TestMaster";
        var modifiedUsername = "TestMaster2";
        var password = "password";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        userService.Create(newUser);
        
        var updatedUser = new User() { Id = newUser.Id, Username = modifiedUsername};
        
        var result = userService.Update(updatedUser);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.Equal(modifiedUsername, result.Username);
    }
    
    [Fact]
    public void UpdateExistingUsersPassword()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var userName = "TestMaster";
        var password = "password";
        var modifiedPassword = "password2";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        userService.Create(newUser);
        
        var updatedUser = new User() { Id = newUser.Id, Password = modifiedPassword};
        
        var result = userService.Update(updatedUser);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.True(PasswordService.VerifyPass(result.Password, modifiedPassword));
    }
    
    [Fact]
    public void UpdateExistingUsersUserNameAndPassword()
    {
        var dbContext = _context.GetDbContext();
        var userService = new UserService(dbContext);
        var userName = "TestMaster";
        var modifiedUsername = "TestMaster2";
        var password = "password";
        var modifiedPassword = "password2";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        userService.Create(newUser);
        
        var updatedUser = new User() { Id = newUser.Id, Username = modifiedUsername, Password = modifiedPassword};
        
        var result = userService.Update(updatedUser);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.Equal(modifiedUsername, result.Username);
        Assert.True(PasswordService.VerifyPass(result.Password, modifiedPassword));
    }
}