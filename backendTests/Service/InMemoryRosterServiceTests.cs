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
}