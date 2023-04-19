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
    public class ShiftController : ControllerBase
    {
        private IShiftService _service;

        public ShiftController(IShiftService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult GetAllShifts()
        {
            return Ok(_service.GetAll());
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetShiftById(int id)
        {
            Shift? shift = _service.GetById(id);
            if (shift != null)
            {
                return Ok(shift);
            }

            return NotFound();
        }
        
        [HttpPost]
        public IActionResult CreateShift([FromBody] Shift shift)
        {
            return Ok(_service.Create(shift));
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteShiftById(int id)
        {
            Shift? shift = _service.Delete(id);
            if (shift != null)
            {
                return Ok(shift);
            }

            return NotFound();
        }
        
        [HttpPut("{id:int}")]
        public IActionResult UpdateShift(int id, [FromBody] Shift updatedShift)
        {
            if (updatedShift.ShiftId == id)
            {
                Shift? shift = _service.Update(updatedShift);
                if (shift != null)
                {
                    return Ok(shift);
                }

                return NotFound();
            }

            return BadRequest("IDs do not match.");
        }
    }
}
