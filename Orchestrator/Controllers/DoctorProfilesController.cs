using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Common.Attributes;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Services.Interfaces;
using ServiceExtensions.Attributes;

namespace Orchestrator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorProfilesController : Controller
{
    private readonly IDoctorProfilesService _service;
    
    public DoctorProfilesController(IDoctorProfilesService service)
    {
        _service = service;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [Authorize(Roles="Receptionist")]
    public async Task<ActionResult<string>> Create([FromForm]CreateDoctorProfileAndPhotoRequest request)
    {
        var token = HttpContext.Request.Headers["Authorization"];
        return Ok(await _service.CreateAsync(request,token));
    }
    
    [HttpPut]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [Authorize(Roles="Receptionist")]
    public async Task<ActionResult> Update([FromForm]EditDoctorProfileAndPhotoRequest request)
    {
        var token = HttpContext.Request.Headers["Authorization"];
        await _service.UpdateAsync(request,token);
        return Ok();
    }
       
    

}