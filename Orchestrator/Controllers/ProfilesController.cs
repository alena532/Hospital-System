using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Common.Attributes;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class ProfilesController : Controller
{
    private readonly IProfilesService _service;
    
    public ProfilesController(IProfilesService service)
    {
        _service = service;
    }

    [HttpPost("CreatePatientProfile")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult> CreatePatientProfile([FromForm]PatientProfileRequest request)
    {
        await _service.CreatePatientProfileAsync(request);
        return Ok();
    }
       

}