﻿using backend.Model;

namespace backend.Repositories;

public class ShiftRepo : IRepository<Shift>
{
    private readonly List<Shift> _listOfShifts;

    public ShiftRepo()
    {
        _listOfShifts = PopulateShiftList();
    }

    private List<Shift> PopulateShiftList()
    {
        return new List<Shift>()
        {
            new Shift("morning", 3, 1),
            new Shift("afternoon", 3, 1),
            new Shift("evening", 2, 1.20)
        };
    }

    public Shift Create(Shift item)
    {
        _listOfShifts.Add(item);
        return item;
    }

    public IEnumerable<Shift> GetAll()
    {
        return _listOfShifts;
    }

    public Shift? GetById(int id)
    {
        return _listOfShifts.FirstOrDefault(shift => shift.ShiftId == id);
    }

    public Shift Delete(int id)
    {
        Shift shift = _listOfShifts.First(shift => shift.ShiftId == id);
        _listOfShifts.Remove(shift);
        return shift;
    }

    public Shift Update(Shift updatedData)
    {
        Shift shift = _listOfShifts.First(shift => shift.ShiftId == updatedData.ShiftId);
        shift.TimeOfShift = updatedData.TimeOfShift;
        shift.NursesRequiredForShift = updatedData.NursesRequiredForShift;
        shift.BonusRate = updatedData.BonusRate;
        return shift;
    }
}