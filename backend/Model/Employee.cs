namespace backend.Model;

public class Employee
{
    public int EmployeeId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly DateOfBirth { get; }

    // TODO after Shift Model is created
    // public List<Shift> PreferredShift { get; }

    public int WorkingDays { get; }
    public int TotalVacationDays { get; }
    
    // TODO after EmployeeType Model is created
    // public EmployeeType EmployeeType { get; }
    
    // access right -> TODO: authentication

    public bool EmploymentStatus { get; }
    public int MonthlyGrossSalary { get; }
    public bool IsDeleted { get; set; }

    public Employee(int employeeId, string firstName, string lastName, DateOnly dateOfBirth, int workingDays, 
        int totalVacationDays, bool employmentStatus, int monthlyGrossSalary, bool isDeleted)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        WorkingDays = workingDays;
        TotalVacationDays = totalVacationDays;
        EmploymentStatus = employmentStatus;
        MonthlyGrossSalary = monthlyGrossSalary;
        IsDeleted = isDeleted;
    }
}