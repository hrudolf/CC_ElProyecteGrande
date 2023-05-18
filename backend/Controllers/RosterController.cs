using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RosterController : ControllerBase
    {
        private IRosterService _service;

        public RosterController(IRosterService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Roster>> GetAllRoster()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Roster> GetRosterById(int id)
        {
            Roster? roster = _service.GetById(id);
            if (roster != null)
            {
                return Ok(roster);
            }

            return NotFound();
        }
        
        [HttpGet("employee/{id:int}")]
        public ActionResult<IEnumerable<Roster>> GetRostersByEmployeeId(int id)
        {
            IEnumerable<Roster>? roster = _service.GetRostersByEmployeeId(id);
            return Ok(roster);

        }


        [HttpPost("GenerateWeeklyRoster")]
        public IActionResult GenerateWeeklyRoster([FromBody] DateTime date)
        {
            if (_service.GenerateWeeklyRoster(date))
            {
                return Ok(_service.GetAll().ToList());
            }

            return Problem("Please choose a Monday for weekly roster generation");
        }
        
        [HttpPost]
        public ActionResult<Roster> CreateRosterItem([FromBody] RosterDto rosterItem)
        {
            Roster? roster = _service.ConvertFromDto(rosterItem);
            if (roster == null) return NotFound();
            return Ok(_service.Create(roster));
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Roster> DeleteRosterItemById(int id)
        {
            Roster? roster = _service.Delete(id);
            if (roster != null)
            {
                return Ok(roster);
            }

            return NotFound();
        }

        [HttpPut]
        public ActionResult<Roster> UpdateRosterItem([FromBody] Roster updatedRosterItem)
        {
            Roster? rosterItem = _service.Update(updatedRosterItem);
            if (rosterItem != null)
            {
                return Ok(rosterItem);
            }

            return NotFound();
        }
        
        [HttpPatch("{id:int}")]
        public ActionResult<IEnumerable<Roster>> ChangeAttendance(int id)
        {
            Roster? attendance = _service.ChangeAttendance(id);
            if (attendance != null)
            {
                return Ok(_service.GetAll());
            }

            return NotFound();
        }
        
    }
}