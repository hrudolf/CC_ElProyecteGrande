using backend.Database;
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

        [HttpGet]
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _service.GetAllEmployees();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            Employee? employee = _service.GetEmployeeById(id);
            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        /*[HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            return Ok(_service.Create(new Employee(employee.FirstName,
                employee.LastName,
                employee.DateOfBirth,
                employee.WorkingDays,
                employee.TotalVacationDays,
                employee.MonthlyGrossSalary,
                employee.EmployeeType,
                employee.PreferredShift)));
        }*/

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployeeById(int id)
        {
            Employee? employee = _service.Delete(id);
            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee updatedEmployee)
        {
            Employee? employee = _service.Update(updatedEmployee);
            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }
    }
}