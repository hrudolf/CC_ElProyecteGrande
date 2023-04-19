using backend.Model;
using backend.Service;
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

    [HttpGet]
    public IActionResult GetAllEmployeeTypes()
    {
        return Ok(_service.GetAll());
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetEmployeeTypeById(int id)
    {
        EmployeeType? employeeType = _service.GetById(id);
        if (employeeType != null)
        {
            return Ok(employeeType);
        }

        return NotFound();
    }
    
    [HttpPost]
    public IActionResult CreateEmployeeType([FromBody] EmployeeType employeeType)
    {
        return Ok(_service.Create(employeeType));
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteEmployeeTypeById(int id)
    {
        EmployeeType? employeeType = _service.Delete(id);
        if (employeeType != null)
        {
            return Ok(employeeType);
        }

        return NotFound();
    }
    
    [HttpPut("{id:int}")]
    public IActionResult UpdateEmployeeType(int id, [FromBody] EmployeeType updatedEmployeeType)
    {
        if (updatedEmployeeType.Id == id)
        {
            EmployeeType? employeeType = _service.Update(updatedEmployeeType);
            if (employeeType != null)
            {
                return Ok(employeeType);
            }

            return NotFound();
        }

        return BadRequest("IDs do not match.");
    }
}