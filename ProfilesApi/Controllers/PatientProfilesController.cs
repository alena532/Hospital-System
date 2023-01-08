using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Common.Attributes;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.Contracts.Responses.PatientProfiles;
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
    
    [HttpPost("CreateAccount")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetAccountUserCredentialsResponse>> CreateAccount(CreatePatientAccountRequest request)
        =>Ok(await _service.CreateAccountAsync(request));
    
    
    //for patient
    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<Guid>> Create([FromBody]CreatePatientProfileRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }

    
    [HttpGet("LinkToAccount/{id:Guid}")]
    public async Task<ActionResult<GetPatientProfilesResponse>> LinkToAccount(Guid id)
        => Ok(await _service.LinkPatientProfileToAccountAsync(id));
    
    
    [HttpGet("Matches/{id:Guid}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<ICollection<GetDoctorProfilesResponse>>> GetMatches(CredentialsPatientProfileRequest request)
        =>Ok(await _service.GetMatchesAsync(request));
    
    
    
    

    
    
}