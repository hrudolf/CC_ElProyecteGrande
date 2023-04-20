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

    public EmployeeType? Delete(int id)
    {
        EmployeeType? employeeInDb = GetById(id);
        if (employeeInDb != null && employeeInDb.GetIsActive())
        {
            return _repository.Delete(id);
        }

        return null;
    }

    public EmployeeType? Update(EmployeeType employeeType)
    {
        EmployeeType? employeeInDb = GetById(employeeType.Id);
        if (employeeInDb != null)
        {
            return _repository.Update(employeeType);
        }

        return null;
    }
}