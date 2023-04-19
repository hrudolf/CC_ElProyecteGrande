using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Http;
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
            return Ok(_service.Create(employee));
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
        
        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            if (updatedEmployee.EmployeeId == id)
            {
                Employee? employee = _service.Update(updatedEmployee);
                if (employee != null)
                {
                    return Ok(employee);
                }

                return NotFound();
            }

            return BadRequest("IDs do not match.");
        }

    }
}
