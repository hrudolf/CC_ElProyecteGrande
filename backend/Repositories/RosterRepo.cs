﻿using backend.Model;

namespace backend.Repositories;

public class RosterRepo : IRepository<Roster>
{
    private readonly HashSet<Roster> _listOfRosters;

    public RosterRepo()
    {
        _listOfRosters = PopulateRosterList();
    }

    private HashSet<Roster> PopulateRosterList()
    {
        return new HashSet<Roster>()
        {
            new Roster(new DateOnly(2023, 04, 19), 1, 1, true),
            new Roster(new DateOnly(2023, 04, 19), 1, 2, true),
            new Roster(new DateOnly(2023, 04, 19), 2, 3, true),
            new Roster(new DateOnly(2023, 04, 19), 2, 4, true),
            new Roster(new DateOnly(2023, 04, 19), 3, 5, true),
            new Roster(new DateOnly(2023, 04, 19), 3, 6, true),
            new Roster(new DateOnly(2023, 04, 20), 1, 1, true),
            new Roster(new DateOnly(2023, 04, 20), 1, 2, true),
        };
    }

    public Roster Create(Roster item)
    {
        _listOfRosters.Add(item);
        return item;
    }

    public IEnumerable<Roster> GetAll()
    {
        return _listOfRosters;
    }

    public Roster? GetById(int id)
    {
        return _listOfRosters.FirstOrDefault(roster => roster.RosterId == id);
    }

    public Roster Delete(int id)
    {
        Roster rosterInDb = _listOfRosters.First(employeeType => employeeType.RosterId == id);
        rosterInDb.ChangeIsActive();
        return rosterInDb;
    }

    public Roster Update(Roster updatedData)
    {
        Roster rosterInDb = _listOfRosters.First(roster => roster.RosterId == updatedData.RosterId);
        rosterInDb.Date = updatedData.Date;
        rosterInDb.ShiftId = updatedData.ShiftId;
        rosterInDb.EmployeeId = updatedData.EmployeeId;
        rosterInDb.Attendance = updatedData.Attendance;
        if (rosterInDb.GetIsActive() != updatedData.GetIsActive())
        {
            rosterInDb.ChangeIsActive();
        }

        return rosterInDb;
    }
}