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
    public IActionResult GetAllVacationRequests()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetVacationRequestById(int id)
    {
        VacationRequest? vacationRequest = _service.GetById(id);
        if (vacationRequest != null)
        {
            return Ok(vacationRequest);
        }

        return NotFound();
    }

    [HttpPost]
    public IActionResult CreateVacationRequest([FromBody] VacationRequestDto vacationRequest)
    {
        VacationRequest? request = _service.ConvertFromDto(vacationRequest);
        if (request == null) return NotFound();
        return Ok(_service.Create(request));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteVacationRequest(int id)
    {
        VacationRequest? vacationRequest = _service.Delete(id);
        if (vacationRequest != null)
        {
            return Ok(vacationRequest);
        }

        return NotFound();
    }

    [HttpPut]
    public IActionResult UpDateVacationRequest([FromBody] VacationRequest updatedVacationRequest)
    {
            VacationRequest? vacationRequest = _service.Update(updatedVacationRequest);
            if (vacationRequest != null) return Ok(vacationRequest);
            return NotFound();
    }
    
    [HttpPatch("{id:int}")]
    public IActionResult ChangeVacationRequestApproval(int id)
    {
        VacationRequest? vacationRequest = _service.ChangeApproval(id);
        if (vacationRequest != null)
        {
            return Ok(vacationRequest);
        }

        return NotFound();
    }
}