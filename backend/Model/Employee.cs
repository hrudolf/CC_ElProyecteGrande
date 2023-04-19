namespace backend.Model;

public class Employee
{
    private static int _employeeIdCounter;
    public int EmployeeId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly DateOfBirth { get; }

    // TODO after Shift Model is created
    // public List<Shift> PreferredShift { get; }

    public int WorkingDays { get; }
    public int TotalVacationDays { get; }
    
    public EmployeeType EmployeeType { get; }
    
    // access right -> TODO: authentication

    public bool EmploymentStatus { get; }
    public int MonthlyGrossSalary { get; }
    public bool _isActive = true;

    public Employee(int employeeId, string firstName, string lastName, DateOnly dateOfBirth, int workingDays, 
        int totalVacationDays, bool employmentStatus, int monthlyGrossSalary, bool isDeleted, EmployeeType employeeType)
    {
        EmployeeId = _employeeIdCounter++;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        WorkingDays = workingDays;
        TotalVacationDays = totalVacationDays;
        EmploymentStatus = employmentStatus;
        MonthlyGrossSalary = monthlyGrossSalary;
        _isActive = isDeleted;
        EmployeeType = employeeType;
    }
    
    public bool GetIsActive()
    {
        return _isActive;
    }
    
    public void ChangeIsActive()
    {
        _isActive = !_isActive;
    }
}