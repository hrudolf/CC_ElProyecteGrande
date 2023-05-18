using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeTypeController : ControllerBase
{
    private readonly IEmployeeTypeService _service;

    public EmployeeTypeController(IEmployeeTypeService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin, Accountant")]
    [HttpGet]
    public ActionResult<List<EmployeeType>> GetAllEmployeeTypes()
    {
        return Ok(_service.GetAll());
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public ActionResult<EmployeeType> GetEmployeeTypeById(int id)
    {
        EmployeeType? employeeType = _service.GetById(id);
        if (employeeType != null)
        {
            return Ok(employeeType);
        }

        return NotFound($"EmployeeType with id #{id} could not be found.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult<EmployeeType> CreateEmployeeType(EmployeeTypeDto employeeType)
    {
        return Ok(_service.Create(new EmployeeType { Type = employeeType.Type }));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public ActionResult<EmployeeType> DeleteEmployeeTypeById(int id)
    {
        EmployeeType? employeeType = _service.Delete(id);
        if (employeeType != null)
        {
            return Ok(employeeType);
        }

        return NotFound($"EmployeeType with id #{id} could not be found.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public ActionResult<EmployeeType> UpdateEmployeeType([FromBody] EmployeeType updatedEmployeeType)
    {
        EmployeeType? employeeType = _service.Update(updatedEmployeeType);
        if (employeeType != null)
        {
            return Ok(employeeType);
        }

        return NotFound($"EmployeeType with id #{updatedEmployeeType.Id} could not be found.");
    }
}