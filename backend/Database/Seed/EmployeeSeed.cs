using System.Runtime.InteropServices.JavaScript;
using backend.Model;

namespace backend.Database.Seed;

public class EmployeeSeed
{
    private DataContext _dataContext;
   
    private static List<string> _firstNames = new List<string>()
        { "Danny", "Sandra", "Angela", "Cecilia", "Catherine", "Tiffany", "Catherine", "Cristina", "Carol", "John" };

    private  List<string> _lastNames = new List<string>()
        { "Smith", "McClure", "Glenn", "Coles", "Metcalfe", "Ross", "Marks", "Fowles", "Green", "Garcia" };

    public EmployeeSeed(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void CreateEmployees(int numberOfEmployees)
    {
        var counter = 0;
        while (counter < numberOfEmployees)
        {
            Employee newEmployee = new Employee
            {
                FirstName = _firstNames[Random.Shared.Next(_firstNames.Count)],
                LastName = _lastNames[Random.Shared.Next(_lastNames.Count)],
                DateOfBirth = GetRandomDate(),
                PreferredShift = GetRandomShift(),
                WorkingDays = Random.Shared.Next(20,25),
                TotalVacationDays = Random.Shared.Next(20,25),
                VacationRequests = new List<VacationRequest>(),
                EmployeeType = GetRandomEmployeeType(),
                EmploymentStatus = true,
                MonthlyGrossSalary = Random.Shared.Next(45000,60000)
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
        return start.AddDays(Random.Shared.Next(range));
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