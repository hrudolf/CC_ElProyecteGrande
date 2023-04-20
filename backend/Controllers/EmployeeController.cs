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

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            Employee? employee = _service.GetById(id);
            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPost]
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
        }

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