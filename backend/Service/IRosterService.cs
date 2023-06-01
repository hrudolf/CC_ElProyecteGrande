using backend.DTOs;
using backend.Model;
using backend.Model.Records;

namespace backend.Service;

public interface IRosterService : IService<Roster>
{
    Roster? ConvertFromDto(RosterDto rosterData);
    bool GenerateWeeklyRoster(DateTime firstDayOfWeek);
    Roster? ChangeAttendance(int id);
    IEnumerable<Roster> GetRostersByEmployeeId(int id);
    public List<Forecast> WeeklyForeCast();
}