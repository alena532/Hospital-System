using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Common.Attributes;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class PatientProfilesController : Controller
{
    private readonly IPatientProfilesService _service;
    
    public PatientProfilesController(IPatientProfilesService service)
    {
        _service = service;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult> Create([FromForm]CreatePatientProfileAndPhotoRequest request)
    {
        await _service.CreateAsync(request);
        return Ok();
    }
    

}