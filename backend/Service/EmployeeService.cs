using System.Diagnostics;
using backend.Database;
using backend.DTOs;
using backend.Model;
using backend.Repositories;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace backend.Service;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _context;

    public EmployeeService(DataContext context)
    {
        _context = context;
    }

    
    // Methods with Entity Framework 

    public async Task<List<Employee>> GetAllEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    public Employee? GetEmployeeById(int id)
    {
        return _context.Employees.ToList().FirstOrDefault(employee => employee.Id == id);
    }

    public Employee? DeleteEmployeePermanentlyById(int id)
    {
        Employee? employee = GetEmployeeById(id);

        if (employee == null) return employee;
        _context.Remove(employee);
        _context.SaveChanges();

        return employee;
    }

    public Employee? UpdateEmployee(int id, UpdateEmployeeDto? updateEmployeeDto)
    {
        Employee? employee = GetEmployeeById(id);

        if (updateEmployeeDto == null && employee == null) return employee;
        
        if (updateEmployeeDto?.FirstName != null) employee!.FirstName = updateEmployeeDto.FirstName ;
        if (updateEmployeeDto?.LastName != null) employee!.LastName = updateEmployeeDto.LastName;
        if (updateEmployeeDto?.DateOfBirth != null) employee!.DateOfBirth = updateEmployeeDto.DateOfBirth;
        //TODO employee!.PreferredShift = updateEmployeeDto.PreferredShift;
        employee.WorkingDays = updateEmployeeDto.WorkingDays;
        employee.TotalVacationDays = updateEmployeeDto.TotalVacationDays;
        //TODO employee!.VacationRequests = updateEmployeeDto.VacationRequests;
        //TODO employee!.EmployeeType = updateEmployeeDto.EmployeeType;
        employee.EmploymentStatus = updateEmployeeDto.EmploymentStatus;
        employee.MonthlyGrossSalary = updateEmployeeDto.MonthlyGrossSalary;
        employee.IsActive = updateEmployeeDto.IsActive;

        _context.SaveChanges();

        return employee;
    }

    public Employee? DeleteEmployeeTemporarilyById(int id)
    {
        Employee? employee = GetEmployeeById(id);
        
        if (employee == null) return employee;

        employee.IsActive = !employee.IsActive;
        _context.SaveChanges();

        return employee;
    }

    public Employee CreateEmployee(UpdateEmployeeDto updateEmployeeDto)
    {
        Employee newEmployee = new Employee
        {
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            DateOfBirth = updateEmployeeDto.DateOfBirth,
            // TODO PreferredShift = updateEmployeeDto.PreferredShift,
            WorkingDays = updateEmployeeDto.WorkingDays,
            TotalVacationDays = updateEmployeeDto.TotalVacationDays,
            // TODO VacationRequests = updateEmployeeDto.VacationRequests,
            // TODO EmployeeType = updateEmployeeDto.EmployeeType,
            // EmploymentStatus = updateEmployeeDto.EmploymentStatus,
            MonthlyGrossSalary = updateEmployeeDto.MonthlyGrossSalary,
            // IsActive = updateEmployeeDto.IsActive,

        };

        _context.Employees.Add(newEmployee);
        _context.SaveChanges();
                
        return newEmployee; 
    }
}