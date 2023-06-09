using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VacationRequestController : ControllerBase
{
    private readonly IVacationRequestService _service;

    public VacationRequestController(IVacationRequestService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin, Supervisor, Accountant")]
    [HttpGet]
    public ActionResult<List<VacationRequest>> GetAllVacationRequests()
    {
        return Ok(_service.GetAll());
    }
    
    [Authorize]
    [HttpGet("public")]
    public ActionResult<List<VacationRequest>> GetAllVacationRequestsPublic()
    {
        var vacationList = _service.GetAll().Select(vac =>
        {
            vac.Employee.SalaryPerShift = 0;
            return vac;
        });
        return Ok(vacationList);
    }

    [Authorize(Roles = "Admin, Basic, ShiftLead, Supervisor")]
    [HttpGet("{id:int}")]
    public ActionResult<VacationRequest> GetVacationRequestById(int id)
    {
        VacationRequest? vacationRequest = _service.GetById(id);
        if (vacationRequest != null)
        {
            return Ok(vacationRequest);
        }

        return NotFound();
    }
    
    [Authorize(Roles = "Admin, Basic, ShiftLead, Supervisor")]
    [HttpGet("employee/{id:int}")]
    public ActionResult<IEnumerable<VacationRequest>> GetVacationRequestsByEmployee(int id)
    {
        IEnumerable<VacationRequest> vacationRequests = _service.GetVacationRequestsByEmployee(id);

        return Ok(vacationRequests);
    }

    [Authorize(Roles = "Admin, Supervisor, ShiftLead, Basic")]
    [HttpGet("approved/{id:int}")]
    public int GetApprovedVacationDays(int id)
    {
        return _service.GetVacationDaysApproved(id);
    }
    
    [Authorize(Roles = "Admin, Supervisor, ShiftLead, Basic")]
    [HttpGet("pending/{id:int}")]
    public int GetPendingVacationDays(int id)
    {
        return _service.GetVacationDaysPending(id);
    }

    [Authorize(Roles = "Admin, Basic, ShiftLead, Supervisor")]
    [HttpPost]
    public ActionResult<VacationRequest> CreateVacationRequest([FromBody] VacationRequestDto vacationRequest)
    {
        VacationRequest? request = _service.ConvertFromDto(vacationRequest);
        if (request == null) return NotFound();
        return Ok(_service.Create(request));
    }

    [Authorize(Roles = "Admin, Basic, ShiftLead, Supervisor")]
    [HttpDelete("{id:int}")]
    public ActionResult<VacationRequest> DeleteVacationRequest(int id)
    {
        VacationRequest? vacationRequest = _service.Delete(id);
        if (vacationRequest != null)
        {
            return Ok(vacationRequest);
        }

        return NotFound();
    }

    [Authorize(Roles = "Admin, Basic, ShiftLead, Supervisor")]
    [HttpPut]
    public ActionResult<VacationRequest> UpDateVacationRequest([FromBody] VacationRequest updatedVacationRequest)
    {
            VacationRequest? vacationRequest = _service.Update(updatedVacationRequest);
            if (vacationRequest != null) return Ok(vacationRequest);
            return NotFound();
    }
    
    [Authorize(Roles = "Admin, Supervisor")]
    [HttpPatch("{id:int}")]
    public ActionResult<IEnumerable<VacationRequest>> ChangeVacationRequestApproval(int id)
    {
        VacationRequest? vacationRequest = _service.ChangeApproval(id);
        if (vacationRequest != null)
        {
            return Ok(_service.GetAll());
        }

        return NotFound();
    }
}