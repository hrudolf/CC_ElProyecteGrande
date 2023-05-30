using backend.Database;
using backend.Model;
using backend.Service;

namespace backendTests.Service;

public class InMemoryRosterServiceTests
{
    [Fact]
    public async void RosterService_Create_ReturnRoster()
    {
        var dbContext = await Context.GetDbContext();
        
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
    public async void RosterService_GetById_ReturnRoster()
    {
        var dbContext = await Context.GetDbContext();
        
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
    public async void RosterService_Delete()
    {
        var dbContext = await Context.GetDbContext();
        
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
    public async void RosterService_GetRostersByEmployeeId_ReturnRosters()
    {
        var dbContext = await Context.GetDbContext();
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

    public Roster NewRosterItem(DataContext dbContext, Employee employee)
    {
        return new Roster()
        {
            Date = DateTime.Now,
            Shift = dbContext.Shifts.FirstOrDefault(),
            Attendance = false,
            Employee = employee,
            Warning = null
        };
    }
}