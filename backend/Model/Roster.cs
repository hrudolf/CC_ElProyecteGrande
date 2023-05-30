using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

namespace backend.Model;

public class Roster
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Shift? Shift { get; set; }
    public Employee? Employee { get; set; }
    public bool Attendance { get; set; }
    public string? Warning { get; set; }
    public bool _isActive = true;

    /*public Roster(DateTime date, Shift shift, Employee employee, bool attendance)
    {
        Date = date;
        Shift = shift;
        Employee = employee;
        Attendance = attendance;
    }
    
    
    [JsonConstructor]
    public Roster(int rosterId, DateTime date, Shift shift, Employee employee, bool attendance)
    {
        Id = rosterId;
        Date = date;
        Shift = shift;
        Employee = employee;
        Attendance = attendance;
    }*/
    
    public bool GetIsActive()
    {
        return _isActive;
    }
    
    public void ChangeIsActive()
    {
        _isActive = !_isActive;
    }
}    