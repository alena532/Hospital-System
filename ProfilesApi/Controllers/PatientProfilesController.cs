using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Common.Attributes;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class PatientProfilesController:ControllerBase
{
    private readonly IPatientProfilesService _service;
    
    
    public PatientProfilesController(IPatientProfilesService service)
    {
        _service = service;
    } 
    
    [HttpGet("matches/{id:Guid}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<ICollection<GetDoctorProfilesResponse>>> GetMatches(Guid id)
    {
        return Ok(await _service.GetMatchesAsync(id));
    }
    
    
    //for patient
    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult> Create(CreatePatientProfileRequest request)
    { 
        _service.CreateAsync(request);
        return Ok(StatusCode(201));
    }
    
    
    
    

    
    
}