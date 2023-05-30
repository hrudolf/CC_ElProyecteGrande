using backend.DTOs;
using backend.Service;

namespace backendTests.Service;

public class InMemoryEmployeeServiceTests
{
        [Fact]
        public async void EmployeeRepository_Add()
        {
            var dbContext = await Context.GetDbContext();

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
            Assert.Equal(result, originalEmployeesCount+1);
        }
}