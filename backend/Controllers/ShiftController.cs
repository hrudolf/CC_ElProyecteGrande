using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _service;

        public ShiftController(IShiftService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Shift>> GetAllShifts()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Shift> GetShiftById(int id)
        {
            Shift? shift = _service.GetById(id);
            if (shift != null)
            {
                return Ok(shift);
            }

            return NotFound($"Shift with id #{id} could not be found.");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Shift> CreateShift(ShiftDto shift)
        {
            return Ok(_service.Create(new Shift
            {
                NameOfShift = shift.NameOfShift,
                NursesRequiredForShift = shift.NursesRequiredForShift,
                BonusRate = shift.BonusRate
            }));
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Shift> DeleteShiftById(int id)
        {
            Shift? shift = _service.Delete(id);
            if (shift != null)
            {
                return Ok(shift);
            }

            return NotFound($"Shift with id #{id} could not be found.");
        }

        [HttpPut]
        public ActionResult<Shift> UpdateShift([FromBody] Shift updatedShift)
        {
            Shift? shift = _service.Update(updatedShift);
            if (shift != null)
            {
                return Ok(shift);
            }

            return NotFound($"Shift with id #{updatedShift.Id} could not be found.");
        }
    }
}