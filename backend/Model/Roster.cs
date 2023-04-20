using System.Text.Json.Serialization;

namespace backend.Model;

public class Roster
{
    private static int _rosterIdCounter;
    public int RosterId { get; }
    public DateOnly Date { get; set; }
    public int ShiftId { get; set; }
    public int EmployeeId { get; set; }
    public bool Attendance { get; set; }
    public bool _isActive = true;

    public Roster(DateOnly date, int shiftId, int employeeId, bool attendance)
    {
        RosterId = _rosterIdCounter++;
        Date = date;
        ShiftId = shiftId;
        EmployeeId = employeeId;
        Attendance = attendance;
    }
    
    
    [JsonConstructor]
    public Roster(int rosterId, DateOnly date, int shiftId, int employeeId, bool attendance)
    {
        RosterId = rosterId;
        Date = date;
        ShiftId = shiftId;
        EmployeeId = employeeId;
        Attendance = attendance;
    }
    
    public bool GetIsActive()
    {
        return _isActive;
    }
    
    public void ChangeIsActive()
    {
        _isActive = !_isActive;
    }
}    