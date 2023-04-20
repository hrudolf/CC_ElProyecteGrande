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
        VacationRequest? requestInDb = _repository.GetById(id);
        if (requestInDb != null)
        {
            return _repository.Delete(id);
        }

        return null;
    }

    public VacationRequest? Update(VacationRequest updatedData)
    {
        VacationRequest? requestInDb = _repository.GetById(updatedData.RequestId);
        if (requestInDb != null)
        {
            return _repository.Update(updatedData);
        }

        return null;
    }

    public VacationRequest? ChangeApproval(int id)
    {
        VacationRequest? requestInDb = _repository.GetById(id);
        if (requestInDb != null)
        {
            return _repository.Update(new VacationRequest(
                requestInDb.RequestId,
                requestInDb.EmployeeId,
                requestInDb.StartDate,
                requestInDb.EndDate,
                !requestInDb.IsApproved));
        }

        return null;
    }
}