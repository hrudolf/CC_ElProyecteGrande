﻿using System.Diagnostics;
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

    
    public List<Employee> GetAllEmployees()
    {
        var employees = _context.Employees
            .Include(e => e.EmployeeType)
            .Include(e => e.PreferredShift)
            .Include(e => e.VacationRequests)
            .ToList();
            
        return employees;
    }

    public List<Employee> GetAllActiveEmployees()
    {
        var employees = _context.Employees
            .Where(employee => employee.IsActive == true)
            .Include(e => e.EmployeeType)
            .Include(e => e.PreferredShift)
            .Include(e => e.VacationRequests)
            .ToList();
            
        return employees;
    }

    public Employee? GetEmployeeById(int id)
    {
        var employees = _context.Employees
            .Where(employee => employee.Id == id)
            .Include(e => e.EmployeeType)
            .Include(e => e.PreferredShift)
            .Include(e => e.VacationRequests)
            .ToList()
            .FirstOrDefault();
        return employees;
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
        
        Shift preferred = _context.Shifts.Where(shift => shift.Id == updateEmployeeDto.PreferredShift.Id)
            .ToList()
            .FirstOrDefault();
        employee!.PreferredShift = preferred;
        
        employee.WorkingDays = updateEmployeeDto.WorkingDays;
        employee.TotalVacationDays = updateEmployeeDto.TotalVacationDays;

        EmployeeType employeeType = _context.EmployeeTypes.Where(type => type.Id == updateEmployeeDto.EmployeeType.Id)
            .ToList()
            .FirstOrDefault();
        employee!.EmployeeType = employeeType;
        
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