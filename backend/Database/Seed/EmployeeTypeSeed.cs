using backend.Model;

namespace backend.Database.Seed;

public class EmployeeTypeSeed
{
    private readonly DataContext _context;

    public EmployeeTypeSeed(DataContext context)
    {
        _context = context;
    }

    public void Create()
    {
        var employeeTypeList = new List<EmployeeType>
        {
            new EmployeeType { Type = "Accountant" },
            new EmployeeType { Type = "Head Nurse" },
            new EmployeeType { Type = "Shift lead nurse" },
            new EmployeeType { Type = "Nurse" },
            new EmployeeType { Type = "Nursing intern" }
        };

        foreach (var employeeType in employeeTypeList)
        {
            _context.EmployeeTypes.Add(employeeType);
        }

        _context.SaveChanges();
    }
}