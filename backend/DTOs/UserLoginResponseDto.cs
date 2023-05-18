using backend.Model;

namespace backend.DTOs;

public class UserLoginResponseDto
{
    public int UserId { get; set;}
    
    public UserRole Role { get; set; }
    
    public int EmployeeId { get; set;}

    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;
}