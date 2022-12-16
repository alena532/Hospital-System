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
    private readonly ILogger<OfficesController> _logger;
    
    public OfficeReceptionistsController(IOfficeReceptionistsService receptionistsService,IHttpContextAccessor httpContextAccessor,ILogger<OfficesController> logger)
    {
        _receptionistsService = receptionistsService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    } 
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetOfficeResponse>>> GetAllForOffice(Guid officeId)
    {
       return Ok(await _receptionistsService.GetAllForOfficeAsync(officeId));
    }

    [HttpGet("{id:Guid}", Name = "GetOfficeReceptionistById")]
    [ServiceFilter(typeof(ValidationOfficeReceptionistExistsAttribute))]

    public async Task<ActionResult<GetOfficeResponse>> GetByIdForOffice(Guid officeId,Guid id)
    {
        _logger.LogInformation($"Getting receptionist {id} for office office {officeId}");
        var receptionist = _httpContextAccessor.HttpContext.Items["receptionist"] as OfficeReceptionist;

        return Ok(await _receptionistsService.GetByIdForOfficeAsync(receptionist));
    }

    [HttpDelete("{id:Guid}")]
    [ServiceFilter(typeof(ValidationOfficeReceptionistExistsAttribute))]
    
    public async Task<IActionResult> DeleteFromOffice(Guid officeId,Guid id)
    {
        _logger.LogInformation($"Deleting receptionist {id} from office {officeId}");
        var receptionist = _httpContextAccessor.HttpContext.Items["receptionist"] as OfficeReceptionist;
        
        await _receptionistsService.DeleteFromOfficeAsync(receptionist);
        return NoContent();
    }

    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetOfficeResponse>> CreateForOffice(Guid officeId,[FromBody] CreateOfficeReceptionistRequest request)
    {
        var receptionistReturn = await _receptionistsService.CreateForOfficeAsync(officeId,request);
        return CreatedAtRoute("GetOfficeReceptionistById", new {officeId = officeId,id = receptionistReturn.Id}, receptionistReturn);
    }
    

    [HttpPut("{id:Guid}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [ServiceFilter(typeof(ValidationOfficeReceptionistExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> UpdateForOffice(Guid officeId,Guid id, [FromBody]EditOfficeReceptionistRequest request)
    {
        var receptionist = HttpContext.Items["receptionist"] as OfficeReceptionist;

        _receptionistsService.UpdateForOfficeAsync(receptionist, request);

        return CreatedAtRoute("GetOfficeReceptionistById", new {officeId = officeId,id = receptionist.Id}, receptionist);
    }
    
}