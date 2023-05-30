using backend.Database;
using backend.Database.Seed;
using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.EntityFrameworkCore;

namespace backendTests.Service;

public class InMemoryEmployeeServiceTests
{
    private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            DataSeed dataSeed = new DataSeed(databaseContext);
            dataSeed.CreateAll();
            return databaseContext;
        }

        [Fact]
        public async void EmployeeRepository_Add()
        {
            var dbContext = await GetDbContext();

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