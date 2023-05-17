using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string LoginName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public virtual Employee? Employee { get; set; }
}