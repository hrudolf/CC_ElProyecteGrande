using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Model;

public class Shift
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string NameOfShift { get; set; } = String.Empty;
    public int NursesRequiredForShift { get; set; }
    public double BonusRate { get; set; }
    [JsonIgnore] public List<Roster> RosterList { get; set; } = new();
    [JsonIgnore] public List<Employee> EmployeeList { get; set; } = new();
}