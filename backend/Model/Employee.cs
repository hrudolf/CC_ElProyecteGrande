namespace backend.Model;

public class Employee
{
    private static int _employeeIdCounter;
    public int EmployeeId { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }

    // TODO after Shift Model is created
    // public List<Shift> PreferredShift { get; }

    public int WorkingDays { get; set; }
    public int TotalVacationDays { get; set; }

    public EmployeeType EmployeeType { get; set; }
    
    // access right -> TODO: authentication

    public bool EmploymentStatus { get; set; }
    public int MonthlyGrossSalary { get; set; }
    public bool _isActive = true;

    public Employee(int employeeId, string firstName, string lastName, DateOnly dateOfBirth, int workingDays, 
        int totalVacationDays, bool employmentStatus, int monthlyGrossSalary, bool isActive, EmployeeType employeeType)
    {
        EmployeeId = _employeeIdCounter++;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        WorkingDays = workingDays;
        TotalVacationDays = totalVacationDays;
        EmploymentStatus = employmentStatus;
        MonthlyGrossSalary = monthlyGrossSalary;
        _isActive = isActive;
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