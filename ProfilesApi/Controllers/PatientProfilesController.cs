using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.PatientProfiles;
using ProfilesApi.Services.Interfaces;
using ServiceExtensions.Attributes;

namespace ProfilesApi.Controllers;

[ApiController]
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
    public async Task<ActionResult<GetAccountUserCredentialsResponse>> CreateAccount([FromBody]CreatePatientAccountRequest request)
        =>Ok(await _service.CreateAccountAsync(request));
    
    
    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [Authorize]
    public async Task<ActionResult<GetPatientProfilesResponse>> Create([FromBody]CreatePatientProfileRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }

    
    [HttpGet("LinkToAccount/{id:Guid}")]
    public async Task<ActionResult<GetPatientProfilesResponse>> LinkToAccount(Guid id)
        => Ok(await _service.LinkPatientProfileToAccountAsync(id));
    
    
    [HttpGet("Matches/{id:Guid}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<ICollection<GetPatientProfilesResponse>>> GetMatches(CredentialsPatientProfileRequest request)
        =>Ok(await _service.GetMatchesAsync(request));
    
    [HttpGet]
    public async Task<ActionResult<GetPatientProfilesResponse>> GetAll()
        => Ok(await _service.GetAllAsync());
    
    
    
    

    
    
}