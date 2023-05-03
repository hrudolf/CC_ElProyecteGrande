using backend.DTOs;
using backend.Model;

namespace backend.Service;

public interface IEmployeeService : IService<Employee>
{
    Task<List<Employee>> GetAllEmployees();

    Employee? GetEmployeeById(int id);

    Employee? DeleteEmployeePermanentlyById(int id);

    Employee? UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto);
    
    Employee? DeleteEmployeeTemporarilyById(int id);

    Employee CreateEmployee(UpdateEmployeeDto updateEmployeeDto);
}