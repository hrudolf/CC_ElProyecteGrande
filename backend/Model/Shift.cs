using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Model;

public class Shift
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string TimeOfShift { get; set; }
    public int NursesRequiredForShift { get; set; }
    public double BonusRate { get; set; }
    [JsonIgnore]
    public List<Roster> RosterList { get; set; } = new();
    [JsonIgnore] 
    public List<Employee> EmployeeList { get; set; }

    /*public Shift(string timeOfShift, int nursesRequiredForShift, double bonusRate)
    {
        TimeOfShift = timeOfShift;
        NursesRequiredForShift = nursesRequiredForShift;
        BonusRate = bonusRate;  
    }

    [JsonConstructor]
    public Shift(int shiftId, string timeOfShift, int nursesRequiredForShift, double bonusRate)
    {
        Id = shiftId;
        TimeOfShift = timeOfShift;
        NursesRequiredForShift = nursesRequiredForShift;
        BonusRate = bonusRate;
    }*/
}