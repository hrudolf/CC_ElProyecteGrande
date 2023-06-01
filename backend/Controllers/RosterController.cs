using backend.DTOs;
using backend.Model;
using backend.Model.Records;
using backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RosterController : ControllerBase
    {
        private IRosterService _service;

        public RosterController(IRosterService service)
        {
            _service = service;
        }
        
        [Authorize(Roles = "Admin, Basic, ShiftLead, Supervisor")]
        [HttpGet]
        public ActionResult<List<Roster>> GetAllRoster()
        {
            return Ok(_service.GetAll());
        }

        [Authorize(Roles = "Admin, ShiftLead, Supervisor")]
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
        
        [Authorize(Roles = "Admin, Basic, Supervisor, ShiftLead")]
        [HttpGet("employee/{id:int}")]
        public ActionResult<IEnumerable<Roster>> GetRostersByEmployeeId(int id)
        {
            IEnumerable<Roster>? roster = _service.GetRostersByEmployeeId(id);
            return Ok(roster);

        }

        [Authorize(Roles = "Admin, ShiftLead, Supervisor")]
        [HttpPost("GenerateWeeklyRoster")]
        public IActionResult GenerateWeeklyRoster([FromBody] DateTime date)
        {
            if (_service.GenerateWeeklyRoster(date))
            {
                return Ok(_service.GetAll().ToList());
            }

            return Problem("Please choose a Monday for weekly roster generation");
        }
        
        [Authorize(Roles = "Admin, ShiftLead, Supervisor")]
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

        [Authorize(Roles = "Admin, ShiftLead, Supervisor")]
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
        
        [Authorize(Roles = "Admin, Supervisor, ShiftLead")]
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
        
        [Authorize(Roles = "Admin, Accountant")]
        [HttpGet("forecast")]
        public List<Forecast> GetForecast()
        {
            return _service.WeeklyForeCast();
        }
    }
}