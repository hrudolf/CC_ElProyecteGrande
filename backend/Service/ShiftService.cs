using backend.Database;
using backend.Model;

namespace backend.Service;

public class ShiftService : IShiftService
{
    private readonly DataContext _context;

    public ShiftService(DataContext context)
    {
        _context = context;
    }

    public Shift Create(Shift shift)
    {
        _context.Shifts.Add(shift);
        _context.SaveChanges();
        return shift;
    }

    public IEnumerable<Shift> GetAll()
    {
        return _context.Shifts;
    }

    public Shift? GetById(int id)
    {
        return _context.Shifts.FirstOrDefault(shift => shift.Id == id);
    }

    public Shift? Delete(int id)
    {
        Shift? shiftInDb = GetById(id);
        if (shiftInDb != null)
        {
            //We have to load employees, to allow removal of Shifts (FK restraint)
            var employees = _context.Employees.ToList();
            _context.Shifts.Remove(shiftInDb);
            _context.SaveChanges();
            return shiftInDb;
        }

        return null;
    }

    public Shift? Update(Shift updatedData)
    {
        Shift? shiftInDb = GetById(updatedData.Id);
        if (shiftInDb != null)
        {
            shiftInDb.NameOfShift = updatedData.NameOfShift;
            shiftInDb.NursesRequiredForShift = updatedData.NursesRequiredForShift;
            shiftInDb.BonusRate = updatedData.BonusRate;
            _context.SaveChanges();
            return shiftInDb;
        }

        return null;
    }
}