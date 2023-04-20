using System.Reflection;
using backend.Model;

namespace backend.Repositories;

public class EmployeeTypeRepo : IRepository<EmployeeType>
{
    private readonly List<EmployeeType> _employeeTypes;

    public EmployeeTypeRepo()
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

    public EmployeeType? GetById(int id)
    {
        return _employeeTypes.FirstOrDefault(employeeType => employeeType.Id == id);
    }

    public EmployeeType Update(EmployeeType updatedData)
    {
        EmployeeType employeeInDb = _employeeTypes.First(employeeType => employeeType.Id == updatedData.Id);
        employeeInDb.Type = updatedData.Type;
        return employeeInDb;
    }

    public EmployeeType Delete(int id)
    {
        EmployeeType employeeInDb = _employeeTypes.First(employeeType => employeeType.Id == id);
        employeeInDb.ChangeIsActive();
        return employeeInDb;
    }
}