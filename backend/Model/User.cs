using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Model;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public virtual Employee? Employee { get; set; }

    public List<UserRole> Roles { get; set; } = new List<UserRole>() { UserRole.Basic };
}