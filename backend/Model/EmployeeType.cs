using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace backend.Model;

public class EmployeeType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Type { get; set; } = String.Empty;

    [JsonIgnore] public List<Employee> EmployeeList { get; set; } = new();
    [JsonIgnore] public UserRole UserRole { get; set; } = UserRole.Basic;
}