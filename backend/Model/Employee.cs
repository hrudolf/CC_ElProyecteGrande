﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace backend.Model;

public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set;}
    [Required]
    public string FirstName { get; set; } = String.Empty;
    [Required]
    public string LastName { get; set; } = String.Empty;
    [Required]
    public DateTime DateOfBirth { get; set; }
    public Shift? PreferredShift { get; set; }
    public int WorkingDays { get; set; }
    public int TotalVacationDays { get; set; }
    [JsonIgnore]
    public List<VacationRequest> VacationRequests { get; set; } = new();
    public EmployeeType? EmployeeType { get; set; }
    public bool EmploymentStatus = true;
    public int SalaryPerShift { get; set; }
    public bool IsActive { get; set; } = true;

  
    public bool GetIsActive()
    {
        return IsActive;
    }
    
    public void ChangeIsActive()
    {
        IsActive = !IsActive;
    }
}