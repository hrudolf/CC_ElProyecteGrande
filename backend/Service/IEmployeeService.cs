using backend.Model;

namespace backend.Service;

public interface IEmployeeService : IService<Employee>
{
    Task<List<Employee>> GetAllEmployees();

    Employee? GetEmployeeById(int id);

    Employee? DeleteEmployeeById(int id);
}