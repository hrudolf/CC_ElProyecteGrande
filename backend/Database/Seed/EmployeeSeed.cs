using System.Runtime.InteropServices.JavaScript;
using backend.Model;

namespace backend.Database.Seed;

public class EmployeeSeed
{
    private DataContext _dataContext;
    
    private readonly Random _rnd = new Random();
   
    private int _numberOfEmployees = 20;
    private static List<string> _firstNames = new List<string>()
        { "Danny", "Sandra", "Angela", "Cecilia", "Catherine", "Tiffany", "Catherine", "Cristina", "Carol", "John" };

    private  List<string> _lastNames = new List<string>()
        { "Smith", "McClure", "Glenn", "Coles", "Metcalfe", "Ross", "Marks", "Fowles", "Green", "Garcia" };

    public EmployeeSeed(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void CreateEmployees()
    {
        var counter = 0;
        while (counter < _numberOfEmployees)
        {
            Employee newEmployee = new Employee()
            {
                FirstName = _firstNames[_rnd.Next(_firstNames.Count - 1)],
                LastName = _lastNames[_rnd.Next(_lastNames.Count - 1)],
                DateOfBirth = GetRandomDate(),
                PreferredShift = GetRandomShift(),
                WorkingDays = _rnd.Next(20,25),
                TotalVacationDays = _rnd.Next(20,25),
                VacationRequests = new List<VacationRequest>(),
                EmployeeType = GetRandomEmployeeType(),
                EmploymentStatus = true,
                MonthlyGrossSalary = _rnd.Next(45000,60000)
            };
            _dataContext.Add(newEmployee);
            _dataContext.SaveChanges();

            counter++;
        }
    }

    private DateTime GetRandomDate()
    {
        DateTime start = new DateTime(1970, 01, 01);
        DateTime end = new DateTime(2005, 12, 31);
        int range = (end - start).Days;
        return start.AddDays(_rnd.Next(range));
    }

    private Shift GetRandomShift()
    {
        return null;
    }
    
    private EmployeeType GetRandomEmployeeType()
    {
        return null;
    }
    
}