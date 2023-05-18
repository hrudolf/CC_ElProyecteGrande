using Microsoft.AspNetCore.Identity;

namespace backend.Service;

public static class PasswordService
{
    public static string HashPass(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public static bool VerifyPass(string hashedPass, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPass);
    }
}