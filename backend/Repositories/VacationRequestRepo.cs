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

    public VacationRequest Delete(int id)
    {
        VacationRequest requestInDb = GetById(id)!;
        _vacationRequests.Remove(requestInDb);
        return requestInDb;
    }

    public VacationRequest Update(VacationRequest updatedData)
    {
        VacationRequest requestInDb = GetById(updatedData.RequestId)!;
        requestInDb.UpDateVacationDate(updatedData);
        return requestInDb;
    }
}