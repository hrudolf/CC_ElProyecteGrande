using backend.Model;

namespace backend.Service;

public interface IVacationRequestService: IService<VacationRequest>
{
    VacationRequest? ChangeApproval(int id);
}