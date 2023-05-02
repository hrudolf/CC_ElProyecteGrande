using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Model;

public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Shift PreferredShift { get; set; }
    public int WorkingDays { get; set; }
    public int TotalVacationDays { get; set; }
    public List<VacationRequest> VacationRequests { get; set; } = new();
    public EmployeeType EmployeeType { get; set; }
    public bool EmploymentStatus = true;
    public int MonthlyGrossSalary { get; set; }
    private bool _isActive = true;

    /*public Employee(string firstName, string lastName , DateTime dateOfBirth, int workingDays, 
        int totalVacationDays, int monthlyGrossSalary, EmployeeType employeeType, int preferredShift)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        WorkingDays = workingDays;
        TotalVacationDays = totalVacationDays;
        MonthlyGrossSalary = monthlyGrossSalary;
        EmployeeType = employeeType;
        PreferredShift = preferredShift;
    }*/
    
    /*[JsonConstructor]
    public Employee(int id, string firstName, string lastName , DateTime dateOfBirth, int workingDays, 
        int totalVacationDays, int monthlyGrossSalary, EmployeeType employeeType, int preferredShift)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        WorkingDays = workingDays;
        TotalVacationDays = totalVacationDays;
        MonthlyGrossSalary = monthlyGrossSalary;
        EmployeeType = employeeType;
        PreferredShift = preferredShift;
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