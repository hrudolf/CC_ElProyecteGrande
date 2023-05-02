using backend.Database;
using backend.Model;

namespace backend.Service;

public class VacationRequestService : IVacationRequestService
{
    private readonly DataContext _context;

    public VacationRequestService(DataContext context)
    {
        _context = context;
    }

    public VacationRequest? ConvertFromDTO(VacationRequestDTO vacationRequestData)
    {
        if (vacationRequestData.Id != null)
        {
            var request = _context.VacationRequests.Find(vacationRequestData.Id);
            if (request == null) return null;
            request.UpdateVacationRequest(vacationRequestData);
            _context.SaveChanges();
            return request;
        }
        var employeeInDb = _context.Employees.Find(vacationRequestData.EmployeeId);
        if (employeeInDb == null) return null;

        return new VacationRequest
        {
            Employee = employeeInDb,
            StartDate = vacationRequestData.StartDate,
            EndDate = vacationRequestData.EndDate
        };
    }

    public VacationRequest Create(VacationRequest item)
    {
        _context.VacationRequests.Add(item);
         _context.SaveChanges();
         return item;
    }

    public IEnumerable<VacationRequest> GetAll()
    {

        return _context.VacationRequests;
    }

    public VacationRequest? GetById(int id)
    {
        return _context.VacationRequests.FirstOrDefault(request => request.Id == id);
    }

    public VacationRequest? Delete(int id)
    {
        VacationRequest? requestInDb = GetById(id);
        if (requestInDb != null)
        {
            _context.VacationRequests.Remove(requestInDb);
            _context.SaveChanges();
        }
        return requestInDb;
    }

    public VacationRequest Update(VacationRequest updatedData)
    {
        updatedData.ChangeApproval(false);
        _context.SaveChanges();
        
        return updatedData;
    }

    public VacationRequest? ChangeApproval(int id)
    {
        VacationRequest? requestInDb = GetById(id);
        if (requestInDb != null)
        {
            requestInDb.ChangeApproval(!requestInDb.IsApproved);
            _context.SaveChanges();
        }
        return requestInDb;
    }
}