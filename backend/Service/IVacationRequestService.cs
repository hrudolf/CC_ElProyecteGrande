using backend.DTOs;
using backend.Model;

namespace backend.Service;

public interface IVacationRequestService: IService<VacationRequest>
{
    VacationRequest? ChangeApproval(int id);
    VacationRequest? ConvertFromDto(VacationRequestDto vacationRequestData);

    public int GetVacationDaysApproved(int id);

    public int GetVacationDaysPending(int id);
}