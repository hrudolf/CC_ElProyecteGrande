using backend.DTOs;
using backend.Model;

namespace backend.Service;

public interface IEmployeeService 
{
    List<Employee> GetAllEmployees();
    
    List<Employee> GetAllActiveEmployees();

    Employee? GetEmployeeById(int id);

    Employee? DeleteEmployeePermanentlyById(int id);

    Employee? UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto);
    
    Employee? DeleteEmployeeTemporarilyById(int id);

    Employee CreateEmployee(UpdateEmployeeDto updateEmployeeDto);
    List<Employee> GetAllActiveEmployeesWithPublicData();
}