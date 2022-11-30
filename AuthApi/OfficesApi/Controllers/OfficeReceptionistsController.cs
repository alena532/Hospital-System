using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficesApi.Common.Attributes;
using OfficesApi.Contracts.Requests.OfficeReceptionist;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;
using OfficesApi.Services.Interfaces;

namespace OfficesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/offices/{officeId}/receptionists")]
public class OfficeReceptionistsController:ControllerBase
{
    private readonly IOfficeReceptionistsService _receptionistsService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public OfficeReceptionistsController(IOfficeReceptionistsService receptionistsService,IHttpContextAccessor httpContextAccessor)
    {
        _receptionistsService = receptionistsService;
        _httpContextAccessor = httpContextAccessor;

    } 
    
    [HttpGet("")]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    public async Task<ActionResult<ICollection<GetOfficeResponse>>> GetAllForOfficeAsync(int officeId)
    {
       return Ok(await _receptionistsService.GetAllForOfficeAsync(officeId));
    }

    [HttpGet("{id:int}", Name = "GetOfficeReceptionistById")]
    [ServiceFilter(typeof(ValidationOfficeReceptionistExistsAttribute))]

    public async Task<ActionResult<GetOfficeResponse>> GetByIdForOfficeAsync(int officeId,int id)
    {
        var receptionist = _httpContextAccessor.HttpContext.Items["receptionist"] as OfficeReceptionist;

        return Ok(await _receptionistsService.GetByIdForOfficeAsync(receptionist));
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(ValidationOfficeReceptionistExistsAttribute))]
    
    public async Task<IActionResult> DeleteFromOfficeAsync(int officeId,int id)
    {
        var receptionist = _httpContextAccessor.HttpContext.Items["receptionist"] as OfficeReceptionist;
        
        await _receptionistsService.DeleteFromOfficeAsync(receptionist);
        return NoContent();
    }

    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> CreateForOfficeAsync(int officeId,[FromBody] CreateOfficeReceptionistRequest request)
    {
        var officeReturn = await _receptionistsService.CreateForOfficeAsync(officeId,request);
        return CreatedAtRoute("GetOfficeReceptionistById", new {id = officeReturn.Id}, officeReturn);
    }
    

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [ServiceFilter(typeof(ValidationOfficeReceptionistExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> UpdateForOfficeAsync(int id, [FromBody]EditOfficeReceptionistRequest request)
    {
        var receptionist = HttpContext.Items["receptionist"] as OfficeReceptionist;

        _receptionistsService.UpdateForOfficeAsync(receptionist, request);

        return NoContent();
    }
    
}