using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Common.Attributes;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Controllers;

[ApiController]
[AllowAnonymous]
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
    public async Task<ActionResult<string>> Create([FromForm]CreateDoctorProfileAndPhotoRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }
    
    [HttpPut]
    [Authorize]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult> Update([FromForm]EditDoctorProfileAndPhotoRequest request)
    {
        await _service.UpdateAsync(request);
        return Ok();
    }
       
    

}