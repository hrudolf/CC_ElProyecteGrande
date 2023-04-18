using System.Reflection;
using backend.Model;

namespace backend.Repositories;

public class EmployeeTypeList : IRepository<EmployeeType>
{
    private readonly List<EmployeeType> _employeeTypes;

    public EmployeeTypeList()
    {
        _employeeTypes = CreateBasicTypes();
    }

    private List<EmployeeType> CreateBasicTypes()
    {
        return new List<EmployeeType>()
        {
            new EmployeeType("Accountant"),
            new EmployeeType("Head Nurse"),
            new EmployeeType("Shift lead nurse"),
            new EmployeeType("Nurse"),
            new EmployeeType("Nursing intern")
        };
    }

    public EmployeeType Create(EmployeeType item)
    {
        _employeeTypes.Add(item);
        return item;
    }

    public IEnumerable<EmployeeType> GetAll()
    {
        return _employeeTypes;
    }

    public EmployeeType GetById(int id)
    {
        return _employeeTypes.First(employee => employee.Id == id);
    }

    public EmployeeType Update(EmployeeType item)
    {
        EmployeeType employeeInDb = _employeeTypes.First(employee => employee.Id == item.Id);
        employeeInDb.Type = item.Type;
        employeeInDb.IsDeleted = item.IsDeleted;
        return employeeInDb;
    }

    public EmployeeType DeleteById(int id)
    {
        EmployeeType employeeInDb = _employeeTypes.First(employee => employee.Id == id);
        employeeInDb.IsDeleted = true;
        return employeeInDb;
    }
}