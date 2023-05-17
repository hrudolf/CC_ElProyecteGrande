using backend.DTOs;
using backend.Model;

namespace backend.Service;

public interface IRosterService : IService<Roster>
{
    Roster? ConvertFromDto(RosterDto rosterData);
    bool GenerateWeeklyRoster(DateTime firstDayOfWeek);
}