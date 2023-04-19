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

    public IEnumerable<Shift> GetAll() => _repository.GetAll().Where(shift => shift.GetIsActive());

    public Shift? GetById(int id) => _repository.GetById(id);

    public Shift? Delete(int id) => _repository.Delete(id);

    public Shift? Update(Shift updatedData)
    {
        return _repository.Update(updatedData);
    }
}