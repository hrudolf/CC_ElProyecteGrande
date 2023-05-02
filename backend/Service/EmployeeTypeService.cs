using backend.Database;
using backend.Model;

namespace backend.Service;

public class EmployeeTypeService : IEmployeeTypeService
{
    private readonly DataContext _context;

    public EmployeeTypeService(DataContext context)
    {
        _context = context;
    }

    public EmployeeType Create(EmployeeType employeeType)
    {
        _context.EmployeeTypes.Add(employeeType);
        _context.SaveChanges();
        return employeeType;
    }

    public IEnumerable<EmployeeType> GetAll()
    {
        return _context.EmployeeTypes;
    }

    public EmployeeType? GetById(int id)
    {
        return _context.EmployeeTypes.FirstOrDefault(employeeType => employeeType.Id == id);
    }

    public EmployeeType? Delete(int id)
    {
        EmployeeType? employeeInDb = GetById(id);
        if (employeeInDb != null && employeeInDb.GetIsActive())
        {
            _context.EmployeeTypes.Remove(employeeInDb);
            _context.SaveChanges();
            return employeeInDb;
        }

        return null;
    }

    public EmployeeType? Update(EmployeeType employeeType)
    {
        EmployeeType? employeeInDb = GetById(employeeType.Id);
        if (employeeInDb != null)
        {
            employeeInDb.Type = employeeType.Type;
            _context.SaveChanges();
            return employeeInDb;
        }

        return null;
    }
}