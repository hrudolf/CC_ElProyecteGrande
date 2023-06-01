using backend.Model;
using backend.Service;

namespace backendTests.Service;

public class VacationRequestServiceTest
{
    private Context _context = new Context();
    
    [Fact]
    public void VacationRequestService_Create()
    {
        var dbContext = _context.GetDbContext();

        //Arrange
        var newRequest = new VacationRequest()
        {
            Employee = dbContext.Employees.FirstOrDefault(),
            StartDate = DateTime.Today,
            EndDate = DateTime.Today
        };
        var vacationRequestService = new VacationRequestService(dbContext);

        var originalRequestsPerEmployeeCount = vacationRequestService.GetVacationRequestsByEmployee(newRequest.Employee.Id).Count();
        vacationRequestService.Create(newRequest);
        
        //Act
        var result = vacationRequestService.GetVacationRequestsByEmployee(newRequest.Employee.Id).Count();

        //Assert
        Assert.Equal(result, originalRequestsPerEmployeeCount + 1);
        Assert.NotNull(vacationRequestService.GetById(newRequest.Id));
    }
    [Fact]
    public void VacationRequestService_Delete()
    {
        var dbContext = _context.GetDbContext();

        //Arrange
        var newRequest = new VacationRequest()
        {
            Employee = dbContext.Employees.FirstOrDefault(),
            StartDate = DateTime.Today,
            EndDate = DateTime.Today
        };
        var vacationRequestService = new VacationRequestService(dbContext);

        var originalRequestsCount = vacationRequestService.GetAll().Count();
        vacationRequestService.Create(newRequest);
        vacationRequestService.Delete(newRequest.Id);

        
        //Act
        var result = vacationRequestService.GetAll().Count();

        //Assert
        Assert.Equal(result, originalRequestsCount);
        Assert.Null(vacationRequestService.GetById(newRequest.Id));
    }
}