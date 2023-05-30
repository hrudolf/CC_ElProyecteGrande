using backend.Service;

namespace backendTests.Service;

public class PasswordServiceTest
{
    [Fact]
    public void HashHasCorrectFormat()
    {
        //Act
        var password = "password";
        var hashedPassword = PasswordService.HashPass(password);
        
        var expectedLength = 60;

        Assert.Equal(expectedLength, hashedPassword.Length);
    }
    
    [Fact]
    public void PasswordAndHashDiffer()
    {
        //Act
        var password = "password";
        var hashedPassword = PasswordService.HashPass(password);

        Assert.NotEqual(password, hashedPassword);
    }
    
    [Fact]
    public void PasswordValidationSuccess()
    {
        //Act
        var password = "password";
        var hashedPassword = PasswordService.HashPass(password);
        bool samePassword = PasswordService.VerifyPass(hashedPassword, password);

        Assert.True(samePassword);
    }
}