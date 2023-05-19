using backend.Database;
using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin, Supervisor, Accountant")]
        [HttpGet]
        public List<Employee> GetAllEmployees()
        {
            return _service.GetAllEmployees();
        }
        
        [Authorize(Roles = "Admin, Supervisor, Accountant")]
        [HttpGet("active")]
        public List<Employee> GetAllActiveEmployees()
        {
            return _service.GetAllActiveEmployees();
        }
        
        [Authorize]
        [HttpGet("public")]
        public List<Employee> GetAllActiveEmployeesWithPublicData()
        {
            return _service.GetAllActiveEmployeesWithPublicData();
        }

        [Authorize(Roles = "Admin, Supervisor, Accountant")]
        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            Employee? employee = _service.GetEmployeeById(id);
            return employee != null ? Ok(employee) : NotFound("User not found");
            
        }

        [Authorize(Roles = "Admin, Supervisor, Accountant")]
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] UpdateEmployeeDto employeeDto)
        {
            return Ok(_service.CreateEmployee(employeeDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployeePermanentlyById(int id)
        {
            Employee? employee = _service.DeleteEmployeePermanentlyById(id);
            return employee != null ? Ok(employee) : NotFound("User not found");
        }

        
        [Authorize(Roles = "Admin, Supervisor, Accountant")]
        [HttpPatch("temporary-delete/{id:int}")]
        public IActionResult DeleteEmployeeTemporarilyById(int id)
        {
            Employee? employee = _service.DeleteEmployeeTemporarilyById(id);
            return employee != null ? Ok(employee) : NotFound("User not found");
        }
        
        [Authorize(Roles = "Admin, Supervisor, Accountant")]
        [HttpPatch("{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updatedEmployee)
        {
            Employee? employee = _service.UpdateEmployee(id, updatedEmployee);
            return employee != null ? Ok(employee) : NotFound("User not found");
        }
    }
}