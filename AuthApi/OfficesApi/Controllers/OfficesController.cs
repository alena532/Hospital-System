using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficesApi.Common.Attributes;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;
using OfficesApi.Services.Interfaces;

namespace OfficesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class OfficesController:ControllerBase
{
    private readonly IOfficesService _officesService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<OfficesController> _logger;
    
    public OfficesController(IOfficesService officesService,IHttpContextAccessor httpContextAccessor,ILogger<OfficesController> logger)
    {
        _officesService = officesService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    } 
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetOfficeResponse>>> GetAllAsync()
    {
       return Ok(await _officesService.GetAllAsync());
    }

    [HttpGet("{id:int}", Name = "GetById")]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]

    public async Task<ActionResult<GetOfficeResponse>> GetByIdAsync(int id)
    {
        _logger.LogInformation($"Getting office {id}");
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;

        return Ok(await _officesService.GetByIdAsync(office));
    }
    
    
    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<IActionResult> DeleteAsync(int id)
    {
        _logger.LogInformation($"Deleting office {id}");
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;
        
        await _officesService.DeleteAsync(office);
        return NoContent();
    }

    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> CreateAsync([FromBody] CreateOfficeRequest request)
    {
        _logger.LogInformation($"Creating office");
        var officeReturn = await _officesService.CreateAsync(request);
        return CreatedAtRoute("GetById", new {id = officeReturn.Id}, officeReturn);
    }
    

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> UpdateAsync(int id, [FromBody]EditOfficeRequest request)
    {
        _logger.LogInformation($"Updating office");
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;
        
        var eventReturn = await _officesService.UpdateAsync(office, request);
        return Ok(eventReturn);
    }
    
}