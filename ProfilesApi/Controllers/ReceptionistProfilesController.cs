using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Common.Attributes;
using ProfilesApi.Contracts.ReceptionistProfiles;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Requests.ReceptionistProfiles;
using ProfilesApi.Contracts.Responses.PatientProfiles;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class ReceptionistProfilesController:ControllerBase
{
    private readonly IReceptionistProfilesService _service;
    
    public ReceptionistProfilesController(IReceptionistProfilesService service)
    {
        _service = service;
    } 
    
    
    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetPatientProfilesResponse>> Create([FromBody]CreateReceptionistProfileRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("UserId/{userId:Guid}")]
    public async Task<ActionResult<GetPatientProfilesResponse>> GetByUserId(Guid userId)
    {
        return Ok(await _service.GetByUserIdAsync(userId));
    }
    
    
    

    
    
    
    

    
    
}