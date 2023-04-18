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
        return Ok(_service.GetAllEmployeeTypes());
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetEmployeeTypeById(int id)
    {
        return Ok(_service.GetById(id));
    }
    
    [HttpPost]
    public IActionResult CreateEmployeeType([FromBody] EmployeeType employeeType)
    {
        return Ok(_service.CreateEmployeeType(employeeType));
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteEmployeeTypeById(int id)
    {
        return Ok(_service.DeleteEmployeeType(id));
    }
    
    [HttpPut]
    public IActionResult UpdateEmployeeType([FromBody] EmployeeType employeeType)
    {
        return Ok(_service.UpdateEmployeeType(employeeType));
    }
}