using backend.Model;
using backend.Service;

namespace backendTests.Service;

public class UserServiceTest
{
    [Fact]
    public async void CreateUserReturnsCorrectIdAndBothUserEntitiesAreEqual()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var userName = "TestMaster";
        var password = "password";
        var expectedId = 32; //30 users created by seed + 1 admin

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        User returnedUser = userService.Create(newUser);
        
        Assert.Equal(newUser, returnedUser);
        Assert.Equal(expectedId, returnedUser.Id);
        Assert.Equal(expectedId, newUser.Id);
    }
    
    [Fact]
    public async void GetAllReturnCorrectNumber()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var expectedCount = 31; //30 users created by seed + 1 admin

        var userCount = userService.GetAll().Count();
        
        Assert.Equal(expectedCount, userCount);
    }
    
    [Fact]
    public async void ReturnsUserWithId()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var newUserId = 32; //30 users created by seed + 1 admin
        var userName = "TestMaster";
        var password = "password";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        userService.Create(newUser);
        
        var result = userService.GetById(newUserId);
        
        Assert.NotNull(result);
        Assert.Equal(userName, result.Username);
    }
    
    [Fact]
    public async void ReturnsNullIdNotFound()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var nonExistingId = 32; //30 users created by seed + 1 admin

        var result = userService.GetById(nonExistingId);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void DeleteExistingReturnsCorrectData()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var newUserId = 32; //30 users created by seed + 1 admin
        var userName = "TestMaster";
        var password = "password";

        User newUser = new User()
        {
            Username = userName,
            Password = password,
            Role = UserRole.Admin
        };

        userService.Create(newUser);
        
        var result = userService.Delete(newUserId);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.Equal(userName, result.Username);
    }
    
    [Fact]
    public async void DeleteNonExistingReturnsNull()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var newUserId = 32; //30 users created by seed + 1 admin
        
        var result = userService.Delete(newUserId);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void UpdateNonExistingUserReturnsNull()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var nonExistingUserId = 32; //30 users created by seed + 1 admin
        var updatedUser = new User() { Id = nonExistingUserId };
        
        var result = userService.Update(updatedUser);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void UpdateExistingUsersUsername()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var newUserId = 32; //30 users created by seed + 1 admin
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
        
        var updatedUser = new User() { Id = newUserId, Username = modifiedUsername};
        
        var result = userService.Update(updatedUser);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.Equal(modifiedUsername, result.Username);
    }
    
    [Fact]
    public async void UpdateExistingUsersPassword()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var newUserId = 32; //30 users created by seed + 1 admin
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
        
        var updatedUser = new User() { Id = newUserId, Password = modifiedPassword};
        
        var result = userService.Update(updatedUser);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.True(PasswordService.VerifyPass(result.Password, modifiedPassword));
    }
    
    [Fact]
    public async void UpdateExistingUsersUserNameAndPassword()
    {
        var dbContext = await Context.GetDbContext();
        var userService = new UserService(dbContext);
        var newUserId = 32; //30 users created by seed + 1 admin
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
        
        var updatedUser = new User() { Id = newUserId, Username = modifiedUsername, Password = modifiedPassword};
        
        var result = userService.Update(updatedUser);
        
        Assert.NotNull(result);
        Assert.Equal(newUser, result);
        Assert.Equal(modifiedUsername, result.Username);
        Assert.True(PasswordService.VerifyPass(result.Password, modifiedPassword));
    }
}