using System.Text.Json.Serialization;

namespace backend.Model;

public class Employee
{
    private static int _employeeIdCounter;
    public int EmployeeId { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<int> PreferredShift { get; set; }
    public int WorkingDays { get; set; }
    public int TotalVacationDays { get; set; }
    public int EmployeeType { get; set; }
    public bool EmploymentStatus = true;
    public int MonthlyGrossSalary { get; set; }
    public bool IsActive { get; set; } = true;

    [JsonConstructor]
    public Employee(string firstName, string lastName , DateOnly dateOfBirth, int workingDays, 
        int totalVacationDays, int monthlyGrossSalary, int employeeType, List<int> preferredShift)
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
    
    public bool GetIsActive()
    {
        return IsActive;
    }
    
    public void ChangeIsActive()
    {
        IsActive = !IsActive;
    }
}