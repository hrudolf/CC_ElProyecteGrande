using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace backend.Model;

public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set;}
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public Shift? PreferredShift { get; set; }
    public int WorkingDays { get; set; }
    public int TotalVacationDays { get; set; }
    [JsonIgnore]
    public List<VacationRequest> VacationRequests { get; set; } = new();
    public EmployeeType? EmployeeType { get; set; }
    public bool EmploymentStatus = true;
    public int MonthlyGrossSalary { get; set; }
    public bool IsActive { get; private set; } = true;

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
        return IsActive;
    }
    
    public void ChangeIsActive()
    {
        IsActive = !IsActive;
    }
}