using backend.Model;

namespace backend.Service;

public interface IEmployeeTypeService
{
    IEnumerable<EmployeeType> GetAllEmployeeTypes();
    EmployeeType GetById(int id);
    EmployeeType CreateEmployeeType(EmployeeType employee);
    EmployeeType DeleteEmployeeType(int id);
    EmployeeType UpdateEmployeeType(EmployeeType employee);
}