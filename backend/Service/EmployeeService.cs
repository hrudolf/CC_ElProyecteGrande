using backend.Model;
using backend.Repositories;

namespace backend.Service;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _repository;

    public EmployeeService(IRepository<Employee> repository)
    {
        _repository = repository;
    }

    public Employee Create(Employee item) => _repository.Create(item);

    public IEnumerable<Employee> GetAll() => _repository.GetAll().Where(employee => employee.GetIsActive());

    public Employee? GetById(int id) => _repository.GetById(id);

    public Employee? Delete(int id)
    {
        Employee? employeeInDb = GetById(id);
        if (employeeInDb != null && employeeInDb.GetIsActive())
        {
            _repository.Delete(id);
        }

        return null;
    }

    public Employee? Update(Employee updatedData)
    {
        Employee? employeeInDb = GetById(updatedData.EmployeeId);
        if (employeeInDb != null)
        {
            _repository.Update(updatedData);
        }

        return null;
    }
}