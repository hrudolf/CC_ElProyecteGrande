using backend.Model;

namespace backend.Repositories;

public class EmployeeRepo : IRepository<Employee>
{
    private HashSet<EmployeeType> _listOfEmployees;

    public EmployeeRepo(HashSet<EmployeeType> listOfEmployees)
    {
        _listOfEmployees = listOfEmployees;
    }

    public HashSet<Employee> PopulateEmployeeList()
    {
        return new HashSet<Employee>()
        {
            new Employee(1, "Susan", "Smith", new DateOnly(1996, 02, 15), 5, 25, true, 35000, true,
                new EmployeeType("Head Nurse")),
            new Employee(1, "Angela", "McClure", new DateOnly(1980, 04, 20), 3, 25, true, 30000, true,
                new EmployeeType("Nurse")),
            new Employee(1, "Sandra", "Glenn", new DateOnly(2000, 02, 4), 5, 25, true, 25000, true,
                new EmployeeType("Nurse")),
            new Employee(1, "Cecilia", "Coles", new DateOnly(2010, 12, 15), 5, 20, true, 25000, true,
                new EmployeeType("Nurse")),
            new Employee(1, "Angela", "Metcalfe", new DateOnly(2002, 05, 19), 4, 20, true, 24000, true,
                new EmployeeType("Shift lead nurse")),
            new Employee(1, "Catherine", "Ross", new DateOnly(2005, 03, 5), 4, 25, true, 20000, true,
                new EmployeeType("Nurse")),
            new Employee(1, "Janet", "Marks", new DateOnly(2006, 09, 1), 5, 25, true, 25000, true,
                new EmployeeType("Nurse")),
            new Employee(1, "Cristina", "Fowles", new DateOnly(2007, 02, 15), 5, 20, true, 25000, true,
                new EmployeeType("Nurse")),
            new Employee(1, "Karol", "Green", new DateOnly(2001, 05, 27), 5, 20, true, 24000, true,
                new EmployeeType("Nurse")),
            new Employee(1, "John", "Garcia", new DateOnly(1996, 02, 15), 5, 25, true, 35000, true,
                new EmployeeType("Accountant"))
        };
    }


    public Employee Create(Employee item)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Employee> GetAll()
    {
        throw new NotImplementedException();
    }

    public Employee? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Employee? Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Employee? Update(Employee updatedData)
    {
        throw new NotImplementedException();
    }
}