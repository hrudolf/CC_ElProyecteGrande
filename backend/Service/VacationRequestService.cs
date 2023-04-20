using backend.Model;
using backend.Repositories;

namespace backend.Service;

public class VacationRequestService : IVacationRequestService
{
    private readonly IRepository<VacationRequest> _repository;

    public VacationRequestService(IRepository<VacationRequest> repository)
    {
        _repository = repository;
    }

    public VacationRequest? Approve(int id)
    {
        VacationRequest? requestToApprove = _repository.GetById(id);
        requestToApprove?.ChangeIsApproved(true);
        return _repository.Update(requestToApprove);
    }

    public VacationRequest Create(VacationRequest item)
    {
        return _repository.Create(item);
    }

    public IEnumerable<VacationRequest> GetAll()
    {
        return _repository.GetAll();
    }

    public VacationRequest? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public VacationRequest? Delete(int id)
    {
        VacationRequest? requestInDb = _repository.GetAll().FirstOrDefault(request => request.RequestId == id);
        if (requestInDb != null)
        {
            return _repository.Delete(id);
        }

        return null;
    }

    public VacationRequest? Update(VacationRequest updatedData)
    {
        VacationRequest? requestInDb = _repository.GetAll().FirstOrDefault(request => request.RequestId == updatedData.RequestId);
        if (requestInDb != null)
        {
            return _repository.Update(updatedData);
        }

        return null;
    }
}