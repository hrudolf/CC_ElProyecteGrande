using backend.Model;
using backend.Repositories;

namespace backend.Service;

public class EmployeeTypeService : IEmployeeTypeService
{
    private readonly IRepository<EmployeeType> _repository;

    public EmployeeTypeService(IRepository<EmployeeType> repository)
    {
        _repository = repository;
    }

    public EmployeeType Create(EmployeeType employeeType) => _repository.Create(employeeType);
    public IEnumerable<EmployeeType> GetAll() => _repository.GetAll().Where(employeeType => employeeType.GetIsActive());
    public EmployeeType? GetById(int id) => _repository.GetById(id);
    public EmployeeType? Delete(int id) => _repository.Delete(id);

    public EmployeeType? Update(EmployeeType employeeType)
    {
        return _repository.Update(employeeType);
    }
}