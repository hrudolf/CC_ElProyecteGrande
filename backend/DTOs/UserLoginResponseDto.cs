namespace backend.DTOs;

public class UserLoginResponseDto
{
    public int Id { get; set; }
    public string Role { get; set; } = String.Empty;

    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
}