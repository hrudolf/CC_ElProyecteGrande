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
    private readonly IRepository<Employee> _repository;
    private readonly DataContext _context;

    public EmployeeService(IRepository<Employee> repository, DataContext context)
    {
        _repository = repository;
        _context = context;
    }

    public Employee Create(Employee item) => _repository.Create(item);

    public IEnumerable<Employee> GetAll() => _repository.GetAll().Where(employee => employee.GetIsActive());

    public Employee? GetById(int id) => _repository.GetById(id);

    public Employee? Delete(int id)
    {
        Employee? employeeInDb = GetById(id);
        if (employeeInDb != null && employeeInDb.GetIsActive())
        {
           return _repository.Delete(id);
        }

        return null;
    }

    public Employee? Update(Employee updatedData)
    {
        Employee? employeeInDb = GetById(updatedData.Id);
        if (employeeInDb != null)
        {
           return _repository.Update(updatedData);
        }

        return null;
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
}