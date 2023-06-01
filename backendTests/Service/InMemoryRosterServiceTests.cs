using backend.Database;
using backend.DTOs;
using backend.Model;
using backend.Service;

namespace backendTests.Service;

public class InMemoryRosterServiceTests
{
    [Fact]
    public void RosterService_Create_ReturnRoster()
    {
        var dbContext = Context.GetDbContext();

        // Arrange
        var newRosterItem = new Roster()
        {
            Date = DateTime.Now,
            Shift = dbContext.Shifts.FirstOrDefault(),
            Attendance = false,
            Employee = dbContext.Employees.FirstOrDefault(),
            Warning = null
        };

        RosterService rosterService = new RosterService(dbContext);
        int numberOfRosterItems = dbContext.Rosters.Count();

        // Act
        rosterService.Create(newRosterItem);
        int rosterItemsAfterCreate = dbContext.Rosters.Count();

        // Assert
        Assert.Equal(dbContext.Rosters.Last(), newRosterItem);
    }

    [Fact]
    public void RosterService_GetById_ReturnRoster()
    {
        var dbContext = Context.GetDbContext();

        // Arrange
        var newRosterItem = new Roster()
        {
            Date = DateTime.Now,
            Shift = dbContext.Shifts.FirstOrDefault(),
            Attendance = false,
            Employee = dbContext.Employees.FirstOrDefault(),
            Warning = null
        };


        RosterService rosterService = new RosterService(dbContext);
        rosterService.Create(newRosterItem);
        var id = dbContext.Rosters.Last().Id;

        // Act
        var rosterById = rosterService.GetById(id);

        // Assert

        Assert.Equal(newRosterItem, rosterById);
    }

    [Fact]
    public void RosterService_Delete()
    {
        var dbContext = Context.GetDbContext();

        // Arrange
        var newRosterItem = new Roster()
        {
            Date = DateTime.Now,
            Shift = dbContext.Shifts.FirstOrDefault(),
            Attendance = false,
            Employee = dbContext.Employees.FirstOrDefault(),
            Warning = null
        };


        RosterService rosterService = new RosterService(dbContext);
        rosterService.Create(newRosterItem);
        var id = dbContext.Rosters.Last().Id;

        // Act
        rosterService.Delete(id);
        Roster? result = rosterService.GetById(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void RosterService_Delete_ReturnNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        // Act
        Roster? rosterItem = rosterService.Delete(1);

        // Assert
        Assert.Null(rosterItem);
    }

    [Fact]
    public void RosterService_GetRostersByEmployeeId_ReturnRosters()
    {
        var dbContext = Context.GetDbContext();
        // Arrange
        var employee = dbContext.Employees.First();
        var newRosterItem1 = NewRosterItem(dbContext, employee);
        var newRosterItem2 = NewRosterItem(dbContext, employee);
        var newRosterItem3 = NewRosterItem(dbContext, employee);
        RosterService rosterService = new RosterService(dbContext);
        rosterService.Create(newRosterItem1);
        rosterService.Create(newRosterItem2);
        rosterService.Create(newRosterItem3);
        IEnumerable<Roster> rosterItems = new List<Roster>()
        {
            newRosterItem1,
            newRosterItem2,
            newRosterItem3
        };

        // Act
        var employeeRosterItems = rosterService.GetRostersByEmployeeId(employee.Id);

        // Assert
        Assert.Equivalent(employeeRosterItems, rosterItems);
    }


    [Fact]
    public void RosterService_Update()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        var employee = dbContext.Employees.First();
        var newRosterItem = NewRosterItem(dbContext, employee);
        RosterService rosterService = new RosterService(dbContext);
        rosterService.Create(newRosterItem);

        var originalRosterItem = dbContext.Rosters.Last();
        var updatedRosterItem = dbContext.Rosters.Last();
        updatedRosterItem.Date = DateTime.Now.AddDays(1);

        // Act
        rosterService.Update(updatedRosterItem);

        // Assert
        Assert.Equal(updatedRosterItem, originalRosterItem);
    }

    [Fact]
    public void RosterService_Update_IsActive()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        var employee = dbContext.Employees.First();
        var newRosterItem = NewRosterItem(dbContext, employee);
        RosterService rosterService = new RosterService(dbContext);
        rosterService.Create(newRosterItem);

        var originalRosterItem = dbContext.Rosters.Last();
        var updatedRosterItem = new Roster()
        {
            Id = 1,
            Date = DateTime.Now,
            Shift = dbContext.Shifts.FirstOrDefault(),
            Attendance = false,
            Employee = employee,
            Warning = null,
            _isActive = false
        };

        // Act
        rosterService.Update(updatedRosterItem);

        // Assert
        Assert.Equal(updatedRosterItem._isActive, originalRosterItem._isActive);
    }

    [Fact]
    public void RosterService_Update_ReturnNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);
        var employee = dbContext.Employees.First();
        Roster newRosterItem = NewRosterItem(dbContext, employee);

        // Act
        Roster? rosterItem = rosterService.Update(newRosterItem);

        // Assert
        Assert.Null(rosterItem);
    }

    [Fact]
    public void RosterService_ChangeAttendance()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);
        var employee = dbContext.Employees.First();
        Roster rosterItem = NewRosterItem(dbContext, employee);
        rosterService.Create(rosterItem);

        // Act
        rosterService.ChangeAttendance(1);

        // Assert
        Assert.True(rosterService.GetById(1)!.Attendance);
    }


    [Fact]
    public void RosterService_ChangeAttendance_ReturnNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        // Act
        Roster? rosterItem = rosterService.ChangeAttendance(1);

        // Assert
        Assert.Null(rosterItem);
    }

    [Fact]
    public void RosterService_ConvertFromDto_ReturnRoster()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        RosterDto rosterDto = new RosterDto()
        {
            Date = DateTime.Now,
            ShiftId = dbContext.Shifts.First().Id,
            EmployeeId = dbContext.Employees.First().Id
        };

        // Act
        Roster? newRosterItem = rosterService.ConvertFromDto(rosterDto);

        // Assert
        Assert.NotNull(newRosterItem);
    }

    [Fact]
    public void RosterService_ConvertFromDto_EmployeeIdNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        RosterDto rosterDto = new RosterDto()
        {
            Date = DateTime.Now,
            ShiftId = 0,
            EmployeeId = 0
        };

        // Act
        Roster? newRosterItem = rosterService.ConvertFromDto(rosterDto);

        // Assert
        Assert.Null(newRosterItem);
    }

    [Fact]
    public void RosterService_ConvertFromDto_ShiftIdNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        RosterDto rosterDto = new RosterDto()
        {
            Date = DateTime.Now,
            ShiftId = 0,
            EmployeeId = 1
        };

        // Act
        Roster? newRosterItem = rosterService.ConvertFromDto(rosterDto);

        // Assert
        Assert.Null(newRosterItem);
    }

    [Fact]
    public void RosterService_GenerateWeeklyRoster_ReturnFalse()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);
        var rosterItem = new Roster()
        {
            Id = 1,
            Date = new DateTime(2023, 05, 30),
            Shift = dbContext.Shifts.FirstOrDefault(),
            Attendance = false,
            Employee = dbContext.Employees.First(),
            Warning = null,
            _isActive = false
        };
        rosterService.Create(rosterItem);
        DateTime date = new DateTime(2023, 05, 30);

        // Act
        bool result = rosterService.GenerateWeeklyRoster(date);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RosterService_GenerateWeeklyRoster_ReturnTrue()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);
        DateTime date = new DateTime(2023, 05, 29);

        // Act
        bool result = rosterService.GenerateWeeklyRoster(date);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RosterService_ChooseShiftLeader_ShiftLeaderNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        List<Employee> availableForShift = new List<Employee>();
        DateTime currentDay = DateTime.Now;
        Shift shift = dbContext.Shifts.First();
        List<EmployeeType> employeeTypes = dbContext.EmployeeTypes.ToList();

        // Act
        rosterService.ChooseShiftLeader(availableForShift, currentDay, shift, employeeTypes);
        Roster newRosterItem = dbContext.Rosters.Last();

        // Assert
        Assert.NotNull(newRosterItem.Warning);
    }

    [Fact]
    public void RosterService_ChooseShiftLeader_ShiftLeaderNotNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);
        var shiftLead = dbContext.EmployeeTypes.First(type => type.Type == "Shift lead nurse");
        dbContext.Employees.Add(new Employee()
        {
            FirstName = "Anne",
            LastName = "Thompson",
            EmployeeType = shiftLead
        });

        dbContext.SaveChanges();

        List<Employee> availableForShift = dbContext.Employees.ToList();
        DateTime currentDay = DateTime.Now;
        Shift shift = dbContext.Shifts.First();
        List<EmployeeType> employeeTypes = dbContext.EmployeeTypes.ToList();

        // Act
        rosterService.ChooseShiftLeader(availableForShift, currentDay, shift, employeeTypes);
        Roster newRosterItem = dbContext.Rosters.Last();

        // Assert
        Assert.NotNull(newRosterItem);
        Assert.Null(newRosterItem.Warning);
    }

    [Fact]
    public void RosterService_AddNursesToRoster_EmployeeNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        List<Employee> availableForShift = new List<Employee>();
        DateTime currentDay = DateTime.Now;
        Shift shift = dbContext.Shifts.First();
        List<EmployeeType> employeeTypes = dbContext.EmployeeTypes.ToList();

        // Act
        rosterService.AddNursesToRoster(availableForShift, currentDay, shift, employeeTypes);
        Roster newRosterItem = dbContext.Rosters.Last();

        // Assert
        Assert.NotNull(newRosterItem.Warning);
    }

    [Fact]
    public void RosterService_AddNursesToRoster_EmployeeNotNull()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);

        Shift shift = dbContext.Shifts.First();
        List<EmployeeType> employeeTypes = dbContext.EmployeeTypes.ToList();
        var nurse = employeeTypes.First(type => type.Type == "Nurse");
        dbContext.Employees.Add(new Employee()
        {
            FirstName = "Nurse",
            LastName = "Joy",
            EmployeeType = nurse,
            PreferredShift = shift
        });
        dbContext.SaveChanges();
        
        List<Employee> availableForShift = dbContext.Employees.ToList();
        DateTime currentDay = DateTime.Now;

        // Act
        rosterService.AddNursesToRoster(availableForShift, currentDay, shift, employeeTypes);
        Roster newRosterItem = dbContext.Rosters.Last();

        // Assert
        Assert.Null(newRosterItem.Warning);
    }

    [Fact]
    public void RosterService_EmployeesNotOnHolidayToday()
    {
        // Arrange
        var dbContext = Context.GetDbContext();
        RosterService rosterService = new RosterService(dbContext);
        VacationRequestService vacationRequestService = new VacationRequestService(dbContext);

        IEnumerable<VacationRequest> vacationRequests = dbContext.VacationRequests;
        DateTime currentDay = DateTime.Now;
        IEnumerable<Employee> employees = dbContext.Employees;

        Employee employee = employees.First();

        VacationRequest newRequest = new VacationRequest()
        {
            Employee = employee,
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(1),
        };

        vacationRequestService.Create(newRequest);

        // Act
        List<Employee> notOnHoliday =
            rosterService.EmployeesNotOnHolidayToday(vacationRequests, currentDay, employees);

        Employee? result = notOnHoliday
            .FirstOrDefault(e =>
                e.FirstName == employee.FirstName &&
                e.LastName == employee.LastName &&
                e.Id == employee.Id);

        // Assert
        Assert.Null(result);
    }

    public Roster NewRosterItem(DataContext dbContext, Employee employee)
    {
        return new Roster()
        {
            Date = DateTime.Now,
            Shift = dbContext.Shifts.FirstOrDefault(),
            Attendance = false,
            Employee = employee,
            Warning = null,
            _isActive = true
        };
    }
}