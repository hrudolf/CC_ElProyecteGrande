using backend.Database;
using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly DataContext _context;

        public EmployeeController(IEmployeeService service, DataContext context)
        {
            _service = service;
            _context = context;
        }

        
        [HttpGet("all-employees")]
        public List<Employee> GetAllEmployees()
        {
            return _service.GetAllEmployees();
        }
        
        
        [HttpGet("all-active-employees")]
        public List<Employee> GetAllActiveEmployees()
        {
            return _service.GetAllActiveEmployees();
        }

        
        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            Employee? employee = _service.GetEmployeeById(id);
            return employee != null ? Ok(employee) : NotFound("User not found");
            
        }

        
        [HttpPost("add-employee")]
        public IActionResult CreateEmployee([FromBody] UpdateEmployeeDto employeeDto)
        {
            return Ok(_service.CreateEmployee(employeeDto));
        }

        
        [HttpDelete("permanent-delete/{id:int}")]
        public IActionResult DeleteEmployeePermanentlyById(int id)
        {
            Employee? employee = _service.DeleteEmployeePermanentlyById(id);
            return employee != null ? Ok(employee) : NotFound("User not found");
        }

        
        
        [HttpPatch("temporary-delete/{id:int}")]
        public IActionResult DeleteEmployeeTemporarilyById(int id)
        {
            Employee? employee = _service.DeleteEmployeeTemporarilyById(id);
            return employee != null ? Ok(employee) : NotFound("User not found");
        }
        
        
        [HttpPut("update-employee/{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updatedEmployee)
        {
            Employee? employee = _service.UpdateEmployee(id, updatedEmployee);
            return employee != null ? Ok(employee) : NotFound("User not found");
        }
    }
}