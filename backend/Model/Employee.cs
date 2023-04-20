using System.Text.Json.Serialization;

namespace backend.Model;

public class Employee
{
    private static int _employeeIdCounter;
    public int EmployeeId { get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int PreferredShift { get; set; }
    public int WorkingDays { get; set; }
    public int TotalVacationDays { get; set; }
    public int EmployeeType { get; set; }
    public bool EmploymentStatus = true;
    public int MonthlyGrossSalary { get; set; }
    private bool _isActive = true;

    public Employee(string firstName, string lastName , DateOnly dateOfBirth, int workingDays, 
        int totalVacationDays, int monthlyGrossSalary, int employeeType, int preferredShift)
    {
        EmployeeId = _employeeIdCounter++;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        WorkingDays = workingDays;
        TotalVacationDays = totalVacationDays;
        MonthlyGrossSalary = monthlyGrossSalary;
        EmployeeType = employeeType;
        PreferredShift = preferredShift;
    }
    
    [JsonConstructor]
    public Employee(int employeeId, string firstName, string lastName , DateOnly dateOfBirth, int workingDays, 
        int totalVacationDays, int monthlyGrossSalary, int employeeType, int preferredShift)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        WorkingDays = workingDays;
        TotalVacationDays = totalVacationDays;
        MonthlyGrossSalary = monthlyGrossSalary;
        EmployeeType = employeeType;
        PreferredShift = preferredShift;
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