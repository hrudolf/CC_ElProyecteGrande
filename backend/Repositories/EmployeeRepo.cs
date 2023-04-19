using backend.Model;

namespace backend.Repositories;

public class EmployeeRepo : IRepository<Employee>
{
    private List<Employee> _listOfEmployees;

    public EmployeeRepo()
    {
        _listOfEmployees = PopulateEmployeeList();
    }

    private List<Employee> PopulateEmployeeList()
    {
        return new List<Employee>()
        {
            new Employee("Susan", "Smith", new DateOnly(1996, 02, 15).ToString(), 5, 25, 35000, 
                new EmployeeType("Head Nurse")),
            new Employee("Angela", "McClure", new DateOnly(1980, 04, 20).ToString(), 3, 25, 30000, new EmployeeType("Nurse")),
            new Employee("Sandra", "Glenn", new DateOnly(2000, 02, 4).ToString(), 5, 25, 25000, 
                new EmployeeType("Nurse")),
            new Employee("Cecilia", "Coles", new DateOnly(2010, 12, 15).ToString(), 5, 20, 25000, 
                new EmployeeType("Nurse")),
            new Employee("Angela", "Metcalfe", "2000-12-12", 4, 20, 24000, 
                new EmployeeType("Shift lead nurse")),
            new Employee("Catherine", "Ross", new DateOnly(2005, 03, 5).ToString(), 4, 25, 20000, 
                new EmployeeType("Nurse")),
            new Employee("Janet", "Marks", new DateOnly(2006, 09, 1).ToString(), 5, 25, 25000, 
                new EmployeeType("Nurse")),
            new Employee("Cristina", "Fowles", new DateOnly(2007, 02, 15).ToString(), 5, 20,  25000, 
                new EmployeeType("Nurse")),
            new Employee("Karol", "Green", new DateOnly(2001, 05, 27).ToString(), 5, 20,  24000, 
                new EmployeeType("Nurse")),
            new Employee("John", "Garcia", new DateOnly(1996, 02, 15).ToString(), 5, 25,  35000, 
                new EmployeeType("Accountant"))
        };
    }


    public Employee Create(Employee item)
    {
        _listOfEmployees.Add(item);
        return item;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _listOfEmployees;
    }

    public Employee? GetById(int id)
    {
        return _listOfEmployees.FirstOrDefault(employee => employee.EmployeeId == id);
    }

    public Employee? Delete(int id)
    {
        Employee? employeeInDb = _listOfEmployees.FirstOrDefault(employee => employee.EmployeeId == id);
        if (employeeInDb != null && employeeInDb.GetIsActive()) employeeInDb.ChangeIsActive();
        return employeeInDb;
    }

    public Employee? Update(Employee updatedData)
    {
        Employee? employeeInDb = _listOfEmployees.FirstOrDefault(employee => employee.EmployeeId == updatedData.EmployeeId);
        if (employeeInDb != null)
        {
            employeeInDb.FirstName = updatedData.FirstName;
            employeeInDb.LastName = updatedData.LastName;
            employeeInDb.DateOfBirth = updatedData.DateOfBirth;
            employeeInDb.WorkingDays = updatedData.WorkingDays;
            employeeInDb.TotalVacationDays = updatedData.TotalVacationDays;
            employeeInDb.EmploymentStatus = updatedData.EmploymentStatus;
            employeeInDb.MonthlyGrossSalary = updatedData.MonthlyGrossSalary;
            employeeInDb._isActive = updatedData._isActive;
            employeeInDb.EmployeeType.Id = updatedData.EmployeeType.Id;
            employeeInDb.EmployeeType.Type = updatedData.EmployeeType.Type;
        }

        return employeeInDb;
    }
}