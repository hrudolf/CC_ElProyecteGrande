using backend.DTOs;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacationRequestController : ControllerBase
{
    private readonly IVacationRequestService _service;

    public VacationRequestController(IVacationRequestService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<VacationRequest>> GetAllVacationRequests()
    {
        return Ok(_service.GetAll());
    }

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

    [HttpPost]
    public ActionResult<VacationRequest> CreateVacationRequest([FromBody] VacationRequestDto vacationRequest)
    {
        VacationRequest? request = _service.ConvertFromDto(vacationRequest);
        if (request == null) return NotFound();
        return Ok(_service.Create(request));
    }

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

    [HttpPut]
    public ActionResult<VacationRequest> UpDateVacationRequest([FromBody] VacationRequest updatedVacationRequest)
    {
            VacationRequest? vacationRequest = _service.Update(updatedVacationRequest);
            if (vacationRequest != null) return Ok(vacationRequest);
            return NotFound();
    }
    
    [HttpPatch("{id:int}")]
    public ActionResult<VacationRequest> ChangeVacationRequestApproval(int id)
    {
        VacationRequest? vacationRequest = _service.ChangeApproval(id);
        if (vacationRequest != null)
        {
            return Ok(vacationRequest);
        }

        return NotFound();
    }
}