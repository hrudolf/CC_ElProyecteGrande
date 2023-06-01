using backend.DTOs;
using backend.Service;

namespace backendTests.Service;

public class InMemoryEmployeeServiceTests
{
    [Fact]
    public void EmployeeRepository_Add()
    {
        var dbContext = Context.GetDbContext();

        //Arrange
        var newEmployee = new UpdateEmployeeDto()
        {
            FirstName = "Isaac",
            LastName = "Newton",
            DateOfBirth = DateTime.Today,
            PreferredShift = dbContext.Shifts.FirstOrDefault(),
            WorkingDays = Random.Shared.Next(20, 25),
            TotalVacationDays = Random.Shared.Next(20, 25),
            EmployeeType = dbContext.EmployeeTypes.FirstOrDefault(),
            EmploymentStatus = true,
            MonthlyGrossSalary = Random.Shared.Next(45000, 60000)
        };
        var userService = new UserService(dbContext);
        var employeeService = new EmployeeService(dbContext, userService);

        var originalEmployeesCount = employeeService.GetAllEmployees().Count;
        employeeService.CreateEmployee(newEmployee);


        //Act
        var result = employeeService.GetAllEmployees().Count;

        //Assert
        Assert.Equal(result, originalEmployeesCount + 1);
    }

    [Fact]
    public void EmployeeRepository_Update()
    {
        var dbContext = Context.GetDbContext();

        //Arrange
        var updatedEmployee = new UpdateEmployeeDto()
        {
            FirstName = "Isaac",
            LastName = "Newton",
            DateOfBirth = DateTime.Today,
            PreferredShift = dbContext.Shifts.FirstOrDefault(),
            WorkingDays = Random.Shared.Next(20, 25),
            TotalVacationDays = Random.Shared.Next(20, 25),
            EmployeeType = dbContext.EmployeeTypes.FirstOrDefault(),
            EmploymentStatus = true,
            MonthlyGrossSalary = Random.Shared.Next(45000, 60000)
        };
        var userService = new UserService(dbContext);
        var employeeService = new EmployeeService(dbContext, userService);

        //Act
        var responseData = employeeService.UpdateEmployee(1, updatedEmployee);

        //Assert
        Assert.Equal(updatedEmployee.FirstName, responseData.FirstName);
        Assert.Equal(updatedEmployee.LastName, responseData.LastName);
        Assert.Equal(updatedEmployee.DateOfBirth, responseData.DateOfBirth);
        Assert.Equal(updatedEmployee.PreferredShift, responseData.PreferredShift);
        Assert.Equal(updatedEmployee.WorkingDays, responseData.WorkingDays);
        Assert.Equal(updatedEmployee.TotalVacationDays, responseData.TotalVacationDays);
        Assert.Equal(updatedEmployee.EmployeeType, responseData.EmployeeType);
        Assert.Equal(updatedEmployee.EmploymentStatus, responseData.EmploymentStatus);
    }

    [Fact]
    public void DeleteEmployeeTemporarilyById()
    {
        var dbContext = Context.GetDbContext();

        //Arrange
        var userService = new UserService(dbContext);
        var employeeService = new EmployeeService(dbContext, userService);

        //Act
        var originalEmployeesCount = employeeService.GetAllActiveEmployees().Count;
        employeeService.DeleteEmployeeTemporarilyById(1);
        var updatedEmployeesCount = employeeService.GetAllActiveEmployees().Count;
        var result = employeeService.GetEmployeeById(1).IsActive;

        //Assert
        Assert.Equal(result, false);
        Assert.Equal(updatedEmployeesCount, originalEmployeesCount - 1);
    }

    [Fact]
    public void GetPublicData()
    {
        var dbContext = Context.GetDbContext();

        //Arrange
        var userService = new UserService(dbContext);
        var employeeService = new EmployeeService(dbContext, userService);
        var employeesWithPublicData = employeeService.GetAllActiveEmployeesWithPublicData();

        Assert.NotEmpty(employeesWithPublicData);
        //Assert
        foreach (var employee in employeesWithPublicData)
        {
            Assert.Equal(0, employee.SalaryPerShift);
        }
    }

    [Fact]
    public void DeleteEmployeePermanentlyById()
    {
        var dbContext = Context.GetDbContext();

        //Arrange
        var userService = new UserService(dbContext);
        var employeeService = new EmployeeService(dbContext, userService);

        var originalEmployeesCount = employeeService.GetAllEmployees().Count;
        employeeService.DeleteEmployeePermanentlyById(1);

        //Act
        var result = employeeService.GetAllEmployees().Count;

        //Assert
        Assert.Equal(result, originalEmployeesCount - 1);
    }
}