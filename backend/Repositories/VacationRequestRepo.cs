using backend.Model;

namespace backend.Repositories;

public class VacationRequestRepo : IRepository<VacationRequest>
{
    private readonly List<VacationRequest> _vacationRequests;

    public VacationRequestRepo()
    {
        _vacationRequests = new List<VacationRequest>();
    }
    public VacationRequest Create(VacationRequest item)
    {
        _vacationRequests.Add(item);
        return item;
    }

    public IEnumerable<VacationRequest> GetAll()
    {
        return _vacationRequests;
    }

    public VacationRequest? GetById(int id)
    {
        return _vacationRequests.FirstOrDefault(request => request.RequestId == id);
    }

    public VacationRequest? Delete(int id)
    {
        VacationRequest? requestInDb = _vacationRequests.FirstOrDefault(request => request.RequestId == id);
        if (requestInDb != null && requestInDb.GetIsApproved()) requestInDb.ChangeIsApproved(false);
        return requestInDb;}

    public VacationRequest? Update(VacationRequest updatedData)
    {
        VacationRequest? requestInDb = _vacationRequests.FirstOrDefault(request => request.RequestId == updatedData.RequestId);
        if (requestInDb != null)
        {
            requestInDb.UpDateVacationDate(updatedData);
            requestInDb.ChangeIsApproved(false);
        }
        return requestInDb;
    }
}