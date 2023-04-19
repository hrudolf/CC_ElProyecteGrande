using backend.Model;
using backend.Repositories;

namespace backend.Service;

public class RosterService : IRosterService
{
    private IRepository<Roster> _repository;

    public RosterService(IRepository<Roster> repository)
    {
        _repository = repository;
    }

    public Roster Create(Roster item) => _repository.Create(item);

    public IEnumerable<Roster> GetAll() => _repository.GetAll().Where(employee => employee.GetIsActive());

    public Roster? GetById(int id) => _repository.GetById(id);

    public Roster? Delete(int id) => _repository.Delete(id);

    public Roster? Update(Roster updatedData)
    {
        return _repository.Update(updatedData);
    }
}