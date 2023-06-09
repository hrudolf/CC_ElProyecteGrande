﻿using backend.Model;

namespace backend.DTOs;

public class UpdateEmployeeDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime DateOfBirth { get; set; }
    public Shift? PreferredShift { get; set; }
    public int WorkingDays { get; set; }
    public int TotalVacationDays { get; set; }
    
    public EmployeeType? EmployeeType { get; set; }
    public bool EmploymentStatus = true;
    public int MonthlyGrossSalary { get; set; }
    public bool IsActive { get; private set; } = true;
}