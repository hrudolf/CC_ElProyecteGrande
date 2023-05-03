﻿using backend.Model;

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

    public void CreateEmployees(int numberOfEmployees)
    {
        List<string> firstNames = new List<string>()
        {
            "Danny", "Sandra", "Angela", "Cecilia", "Catherine", "Tiffany", "Catherine", "Cristina", "Carol", "John"
        };

        List<string> lastNames = new List<string>()
            { "Smith", "McClure", "Glenn", "Coles", "Metcalfe", "Ross", "Marks", "Fowles", "Green", "Garcia" };

        var counter = 0;
        while (counter < numberOfEmployees)
        {
            Employee newEmployee = new Employee
            {
                FirstName = firstNames[Random.Shared.Next(firstNames.Count)],
                LastName = lastNames[Random.Shared.Next(lastNames.Count)],
                DateOfBirth = GetRandomDate(),
                PreferredShift = GetRandomShift(),
                WorkingDays = Random.Shared.Next(20, 25),
                TotalVacationDays = Random.Shared.Next(20, 25),
                VacationRequests = new List<VacationRequest>(),
                EmployeeType = GetRandomEmployeeType(),
                EmploymentStatus = true,
                MonthlyGrossSalary = Random.Shared.Next(45000, 60000)
            };
            _context.Add(newEmployee);
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

    private EmployeeType? GetRandomEmployeeType()
    {
        List<EmployeeType> employeeTypes = _context.EmployeeTypes.ToList();
        if (employeeTypes.Count == 0) return null;
        return employeeTypes[Random.Shared.Next(employeeTypes.Count)];
    }
}