using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficesApi.Common.Attributes;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;
using OfficesApi.Services.Interfaces;
using SharedModels.Messages;

namespace OfficesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class OfficesController:ControllerBase
{
    private readonly IOfficesService _officesService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<OfficesController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public OfficesController(IOfficesService officesService,IHttpContextAccessor httpContextAccessor,ILogger<OfficesController> logger,IPublishEndpoint publishEndpoint)
    {
        _officesService = officesService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    } 
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetOfficeResponse>>> GetAllAsync()
    {
       return Ok(await _officesService.GetAllAsync());
    }

    [HttpGet("{id:Guid}", Name = "GetById")]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]

    public async Task<ActionResult<GetOfficeResponse>> GetByIdAsync(Guid id)
    {
        _logger.LogInformation($"Getting office {id}");
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;

        return Ok(await _officesService.GetByIdAsync(office));
    }
    
    
    [HttpDelete("{id:Guid}")]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<IActionResult> DeleteAsync(Guid id)
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
    

    [HttpPut("{id:Guid}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> UpdateAsync(Guid id, [FromBody]EditOfficeRequest request)
    {
        _logger.LogInformation($"Updating office");
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;
        
        var eventReturn = await _officesService.UpdateAsync(office, request);
        _publishEndpoint.Publish<IOfficeUpdated>(new
        {
            Id = eventReturn.Id,
            Address = eventReturn.Address

        });
        return Ok(eventReturn);
    }
    
    [HttpPut("UpdateStatus/{id:Guid}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> UpdateStatusByIdAsync(Guid id, [FromBody]EditOfficeStatusRequest request)
    {
        _logger.LogInformation($"Updating office status");
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;
        
        var eventReturn = await _officesService.UpdateStatusAsync(office, request);
        return Ok(eventReturn);
    }
    
    
    
}