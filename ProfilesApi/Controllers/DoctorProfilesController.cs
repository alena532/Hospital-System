using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Common.Attributes;
using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class DoctorProfilesController:ControllerBase
{
    private readonly IDoctorProfilesService _service;

    public DoctorProfilesController(IDoctorProfilesService service)
    {
        _service = service;
    } 
    
    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetDoctorProfilesResponse>> Create(CreateDoctorProfileRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }
    
    [HttpPost("ConfirmEmail")]
    public async Task<ActionResult> ConfirmEmail(Guid accountId)
    {
        await _service.ConfirmEmailAsync(accountId);
        return Ok();
    }

    [HttpGet("")]
    public async Task<ActionResult<PageResult<GetDoctorAndPhotoProfilesResponse>>> GetAll([FromQuery]int pageNumber,[FromQuery]int pageSize,[FromQuery]SearchAndFilterParameters parameters)
    {
        return Ok(await _service.GetAllAsync(pageNumber,pageSize,parameters));
    }
    
    
}