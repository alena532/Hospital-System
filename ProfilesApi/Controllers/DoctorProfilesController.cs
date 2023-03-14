using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Mail;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.Services.Interfaces;
using ServiceExtensions.Attributes;

namespace ProfilesApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DoctorProfilesController:ControllerBase
{
    private readonly IDoctorProfilesService _service;

    public DoctorProfilesController(IDoctorProfilesService service)
    {
        _service = service;
    } 
    
    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [Authorize(Roles="Receptionist")]
    public async Task<ActionResult<GetMailAndIdStuffResponse>> Create([FromBody]CreateDoctorProfileRequest request)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = new Guid(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
        
        return Ok(await _service.CreateAsync(request,userId));
    }
    
    [HttpPost("ConfirmEmail")]
    public async Task<ActionResult> ConfirmEmail(Guid accountId)
    {
        await _service.ConfirmEmailAsync(accountId);
        return Ok();
    }
    
    [HttpPut]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [Authorize(Roles="Receptionist,Doctor")]
    public async Task<ActionResult<GetDoctorProfilesResponse>> Update([FromBody]EditDoctorProfileRequest request)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = new Guid(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
        
        return Ok(await _service.UpdateAsync(request,userId));
    }

    [HttpGet]
    public async Task<ActionResult<PageResult<GetDoctorAndPhotoProfilesResponse>>> GetPage([FromQuery]int pageNumber,[FromQuery]int pageSize,[FromQuery]SearchAndFilterParameters parameters)
    {
        return Ok(await _service.GetPageAsync(pageNumber,pageSize,parameters));
    }
    
    [HttpGet("CheckEmailConfirmation/{userId:Guid}")]
    public async Task<ActionResult<bool>> CheckEmailConfirmation(Guid userId)
    {
        return Ok(await _service.CheckEmailConfirmation(userId));
    }
    
    [HttpGet("UserId/{userId:Guid}")]
    public async Task<ActionResult<GetDoctorProfilesResponse>> GetByUserId(Guid userId)
    {
        return Ok(await _service.GetByUserIdAsync(userId));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<GetDoctorProfilesResponse>> GetById(Guid id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    
    
}