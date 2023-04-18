﻿using backend.Model;
using backend.Repositories;

namespace backend.Service;

public class EmployeeTypeService : IEmployeeTypeService
{
    private readonly IRepository<EmployeeType> _repository;

    public EmployeeTypeService(IRepository<EmployeeType> repository)
    {
        _repository = repository;
    }

    public IEnumerable<EmployeeType> GetAllEmployeeTypes() => _repository.GetAll();
    public EmployeeType GetById(int id) => _repository.GetById(id);
    public EmployeeType CreateEmployeeType(EmployeeType employee) => _repository.Create(employee);
    public EmployeeType DeleteEmployeeType(int id) => _repository.DeleteById(id);
    public EmployeeType UpdateEmployeeType(EmployeeType employee) => _repository.Update(employee);
}