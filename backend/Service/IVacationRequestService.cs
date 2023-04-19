using backend.Model;

namespace backend.Service;

public interface IVacationRequestService: IService<VacationRequest>
{
    VacationRequest? Approve(int id); 
}