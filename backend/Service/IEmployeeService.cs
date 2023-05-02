using backend.Model;

namespace backend.Service;

public interface IEmployeeService : IService<Employee>
{
    Task<List<Employee>> GetAllEmployees();
}