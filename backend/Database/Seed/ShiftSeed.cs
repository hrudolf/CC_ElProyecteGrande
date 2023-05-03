using backend.Model;

namespace backend.Database.Seed;

public class ShiftSeed
{
    private readonly DataContext _context;

    public ShiftSeed(DataContext context)
    {
        _context = context;
    }

    public void Create()
    {
        var shiftList = new List<Shift>
        {
            new Shift {NameOfShift = "morning", NursesRequiredForShift = 3, BonusRate = 1},
            new Shift {NameOfShift = "afternoon", NursesRequiredForShift = 3, BonusRate = 1},
            new Shift {NameOfShift = "night", NursesRequiredForShift = 2, BonusRate = 1.2}
        };

        foreach (var shift in shiftList)
        {
            _context.Shifts.Add(shift);
        }

        _context.SaveChanges();
    }
}