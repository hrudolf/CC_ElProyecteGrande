using backend.Database;
using backend.DTOs;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Service;

public class VacationRequestService : IVacationRequestService
{
    private readonly DataContext _context;

    public VacationRequestService(DataContext context)
    {
        _context = context;
    }

    public VacationRequest? ConvertFromDto(VacationRequestDto vacationRequestData)
    {
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
        return _context.VacationRequests
            .Include(request => request.Employee)
            .ThenInclude(employee => employee.EmployeeType)
            .Include(request => request.Employee)
            .ThenInclude(employee => employee.PreferredShift);
    }

    public IEnumerable<VacationRequest> GetVacationRequestsByEmployee(int id)
    {
        return _context.VacationRequests.Where(request => request.Employee.Id == id)
            .Include(request => request.Employee)
            .ThenInclude(employee => employee.EmployeeType)
            .Include(request => request.Employee)
            .ThenInclude(employee => employee.PreferredShift);
    }
    public VacationRequest? GetById(int id)
    {
        return GetAll().FirstOrDefault(request => request.Id == id);
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

    public VacationRequest? Update(VacationRequest vacationRequestData)
    {
        VacationRequest? requestInDb = GetById(vacationRequestData.Id);
        if (requestInDb != null)
        {
            requestInDb.UpdateVacationRequest(vacationRequestData);
            requestInDb.ChangeApproval(false);
            _context.SaveChanges();
        }
        return requestInDb;
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

    public int GetVacationDaysApproved(int id)
    {
        int sumOfDaysApproved = 0;
        List <VacationRequest> requests = _context.VacationRequests
            .Where(request => request.Employee.Id == id && request.IsApproved == true)
            .ToList();

        foreach (var request in requests)
        {
            if (request.EndDate > request.StartDate)
            {
                var difference = request.EndDate - request.StartDate;
                sumOfDaysApproved += difference.Days;
            }
            
        }

        return sumOfDaysApproved;
    }
    
    public int GetVacationDaysPending(int id)
    {
        int sumOfDaysApproved = 0;
        List <VacationRequest> requests = _context.VacationRequests
            .Where(request => request.Employee.Id == id && request.IsApproved == false)
            .ToList();

        foreach (var request in requests)
        {
            if (request.EndDate > request.StartDate)
            {
                var difference = request.EndDate - request.StartDate;
                sumOfDaysApproved += difference.Days;
            }
            
        }

        return sumOfDaysApproved;
    }
}