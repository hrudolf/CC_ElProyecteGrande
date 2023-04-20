using backend.Model;
using backend.Repositories;

namespace backend.Service;

public class ShiftService : IShiftService
{
    private readonly IRepository<Shift> _repository;

    public ShiftService(IRepository<Shift> repository)
    {
        _repository = repository;
    }

    public Shift Create(Shift item) => _repository.Create(item);

    public IEnumerable<Shift> GetAll() => _repository.GetAll();

    public Shift? GetById(int id) => _repository.GetById(id);

    public Shift? Delete(int id)
    {
        Shift? shiftInDb = GetById(id);
        if (shiftInDb != null)
        {
            return _repository.Delete(id);
        }

        return null;
    }

    public Shift? Update(Shift updatedData)
    {
        Shift? shiftInDb = GetById(updatedData.ShiftId);
        if (shiftInDb != null)
        {
            return _repository.Update(updatedData);
        }

        return null;
    }
}