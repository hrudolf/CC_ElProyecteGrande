using backend.Database;
using backend.DTOs;
using backend.Model;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Service;

public class RosterService : IRosterService
{
    private readonly DataContext _context;

    public RosterService(DataContext context)
    {
        _context = context;
    }
    public Roster? ConvertFromDto(RosterDto rosterData)
    {
        var employeeInDb = _context.Employees.FirstOrDefault(employee=>employee.Id==rosterData.EmployeeId);
        if (employeeInDb == null) return null;
        var shiftInDb = _context.Shifts.FirstOrDefault(shift=>shift.Id==rosterData.ShiftId);
        if (shiftInDb == null) return null;

        return new Roster
        {
            Date = rosterData.Date,
            Shift = shiftInDb,
            Employee = employeeInDb
        };
    }

    public Roster Create(Roster item)
    {
        _context.Rosters.Add(item);
        _context.SaveChanges();
        return item;
    }

    public IEnumerable<Roster> GetAll() =>
        _context.Rosters
        .Include(roster=>roster.Employee)
        .Include(roster=>roster.Shift)
        .ToList()
        .Where(roster => roster.GetIsActive() == true);

    public Roster? GetById(int id) => GetAll().FirstOrDefault(roster => roster.Id == id);

    public Roster? Delete(int id)
    {
        Roster? rosterInDb = GetById(id);
        if (rosterInDb != null && rosterInDb.GetIsActive())
        {
            rosterInDb.ChangeIsActive();
             _context.SaveChanges();
             return rosterInDb;
        }

        return null;
    }

    public Roster? Update(Roster updatedData)
    {
        Roster? rosterInDb = GetById(updatedData.Id);
        if (rosterInDb != null && rosterInDb.GetIsActive())
        {
            rosterInDb.Date = updatedData.Date;
            rosterInDb.Shift = updatedData.Shift;
            rosterInDb.Employee = updatedData.Employee;
            rosterInDb.Attendance = updatedData.Attendance;
            if (rosterInDb.GetIsActive() != updatedData.GetIsActive())
            {
                rosterInDb.ChangeIsActive();
            }
            _context.SaveChanges();
            return rosterInDb;
        }

        return null;
    }
}