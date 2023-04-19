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
    public IActionResult CreateVacationRequest([FromBody] VacationRequest vacationRequest)
    {
        return Ok(_service.Create(vacationRequest));
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
    
    [HttpPatch("{id:int}")]
    public IActionResult ApproveVacationRequest(int id)
    {
        /*
        if (updatedVacationRequest.RequestId == id)
        {
            */
            VacationRequest? vacationRequest = _service.Approve(id);
            if (vacationRequest != null)
            {
                return Ok(vacationRequest);
            }

            return NotFound();
        /*}

        return BadRequest("IDs do not match.");*/
    }
    [HttpPut("{id:int}")]
    public IActionResult UpDateVacationRequest(int id, [FromBody] VacationRequest updatedVacationRequest)
    {
        if (updatedVacationRequest.RequestId == id)
        {
            VacationRequest? vacationRequest = _service.Update(updatedVacationRequest);
            if (vacationRequest != null)
            {
                return Ok(vacationRequest);
            }

            return NotFound();
        }

        return BadRequest("IDs do not match.");
    }
}