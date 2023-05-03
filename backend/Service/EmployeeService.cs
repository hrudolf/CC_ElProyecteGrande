using backend.Database;
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
}