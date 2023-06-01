using backend.Model;
using backend.Service;

namespace backend.Database.Seed;

public class DataSeed
{
    private readonly DataContext _context;

    public DataSeed(DataContext context)
    {
        _context = context;
    }

    public void CreateAll(int numberOfEmployees = 30)
    {
        CreateShifts();
        CreateEmployeeTypes();
        CreateEmployees(numberOfEmployees);
        CreateVacationRequests(5);
        CreateUsers();
    }

    public void CreateShifts()
    {
        var shiftList = new List<Shift>
        {
            new Shift { NameOfShift = "morning", NursesRequiredForShift = 3, BonusRate = 1 },
            new Shift { NameOfShift = "afternoon", NursesRequiredForShift = 3, BonusRate = 1 },
            new Shift { NameOfShift = "night", NursesRequiredForShift = 2, BonusRate = 1.2 }
        };

        foreach (var shift in shiftList)
        {
            _context.Shifts.Add(shift);
        }

        _context.SaveChanges();
    }

    public void CreateEmployeeTypes()
    {
        var employeeTypeList = new List<EmployeeType>
        {
            new EmployeeType { Type = "Accountant", UserRole = UserRole.Accountant},
            new EmployeeType { Type = "Head Nurse", UserRole = UserRole.Supervisor },
            new EmployeeType { Type = "Shift lead nurse", UserRole = UserRole.ShiftLead },
            new EmployeeType { Type = "Nurse", UserRole = UserRole.Basic },
            new EmployeeType { Type = "Nursing intern", UserRole = UserRole.Basic }
        };

        foreach (var employeeType in employeeTypeList)
        {
            _context.EmployeeTypes.Add(employeeType);
        }

        _context.SaveChanges();
    }

    public void CreateEmployees(int numberOfEmployees)
    {
        List<string> firstNames = new List<string>()
        {
            "Danny", "Sandra", "Angela", "Cecilia", "Catherine", "Tiffany", "Catherine", "Cristina", "Carol", "John",
            "Laura", "Terry", "Sandra", "Linda", "Mary", "Jen", "Monica", "Lisa", "Nancy", "Betty", "Helen", "Doris",
            "Brittany", "Alexis", "Lori", "Claudia", "Charlotte", "Amber", "Logan", "Alan", "Randy", "Vincent"

        };

        List<string> lastNames = new List<string>()
        {
            "Smith", "McClure", "Glenn", "Coles", "Metcalfe", "Ross", "Marks", "Fowles", "Green", "Garcia",
            "Johnson", "Williams", "Miller", "Davis", "Rodrigez", "Anderson", "Moore", "O'Reily", "Jackson",
            "Young", "Walker", "King", "Scott", "Hill", "Carter", "Roberts"
        };

        var counter = 0;
        while (counter < numberOfEmployees)
        {
            Employee newEmployee = new Employee
            {
                FirstName = firstNames[Random.Shared.Next(firstNames.Count)],
                LastName = lastNames[Random.Shared.Next(lastNames.Count)],
                DateOfBirth = GetRandomDate().Date,
                PreferredShift = GetRandomShift(),
                WorkingDays = Random.Shared.Next(20, 25),
                TotalVacationDays = Random.Shared.Next(20, 25),
                VacationRequests = new List<VacationRequest>(),
                EmployeeType = GetRandomEmployeeType(),
                EmploymentStatus = true,
                SalaryPerShift = Random.Shared.Next(150, 300)
            };
            var user = new User
            {
                Username = $"user{counter + 1}",
                Password = PasswordService.HashPass($"user{counter + 1}"),
                Employee = newEmployee,
                Role = newEmployee.EmployeeType.UserRole
            };
            _context.Users.Add(user);
            _context.Add(newEmployee);
            _context.SaveChanges();

            counter++;
        }
    }
    
    private void CreateUsers()
    {
        //TODO add more users and connect them to employees
        Employee adminEmployee = new Employee()
        {
            FirstName = "Mr.",
            LastName = "Admin",
            DateOfBirth = GetRandomDate().Date,
            EmployeeType = GetRandomEmployeeType()
        };
        
        var admin = new User
        {
            Username = "admin",
            Password = PasswordService.HashPass($"admin"),
            Role = UserRole.Admin,
            Employee = adminEmployee
        };
        _context.Employees.Add(adminEmployee);
        _context.Users.Add(admin);
        
        Employee accountantEmployee = new Employee()
        {
            FirstName = "Mr.",
            LastName = "Accountant",
            DateOfBirth = GetRandomDate().Date,
            EmployeeType = GetRandomEmployeeType()
        };
        
        var accountant = new User
        {
            Username = "accountant",
            Password = PasswordService.HashPass($"accountant"),
            Role = UserRole.Accountant,
            Employee = accountantEmployee
        };
        _context.Employees.Add(accountantEmployee);
        _context.Users.Add(accountant);
        _context.SaveChanges();
    }

    public void CreateVacationRequests(int numberOfRequests)
    {
        var counter = 0;
        while (counter < numberOfRequests)
        {
            var startDate = GetRandomDateForVacationRequest();
            VacationRequest vacationRequest = new VacationRequest
            {
                Employee = GetRandomEmployee(),
                StartDate = startDate,
                EndDate = startDate.AddDays(Random.Shared.Next(2, 10))
            };

            _context.Add(vacationRequest);
            _context.SaveChanges();
            
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

    private Shift? GetRandomShift()
    {
        List<Shift> shifts = _context.Shifts.ToList();
        if (shifts.Count == 0) return null;
        return shifts[Random.Shared.Next(shifts.Count)];
    }

    private EmployeeType GetRandomEmployeeType()
    {
        List<EmployeeType> employeeTypes = _context.EmployeeTypes.ToList();
        if (employeeTypes.Count == 0) throw new Exception();
        return employeeTypes[Random.Shared.Next(employeeTypes.Count)];
    }
    
    private Employee GetRandomEmployee()
    {
        List<Employee> employees = _context.Employees.ToList();
        if (employees.Count == 0) throw new Exception("Employee list is empty");
        return employees[Random.Shared.Next(employees.Count)];
    }
    
    private DateTime GetRandomDateForVacationRequest()
    {
        DateTime start = new DateTime(2023, 04, 01);
        DateTime end = new DateTime(2023, 12, 31);
        int range = (end - start).Days;
        return start.AddDays(Random.Shared.Next(range));
    }
}