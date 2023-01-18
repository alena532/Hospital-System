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
    private readonly IOfficesService _service;
    private readonly ILogger<OfficesController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public OfficesController(IOfficesService officesService,ILogger<OfficesController> logger,IPublishEndpoint publishEndpoint)
    {
        _service = officesService;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    } 
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetOfficeResponse>>> GetAll()
        => Ok(await _service.GetAllAsync());
    

    [HttpGet("{id:Guid}", Name = "GetById")]
    public async Task<ActionResult<GetOfficeResponse>> GetById(Guid id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetOfficeResponse>> Create([FromBody] CreateOfficeRequest request)
    {
        var officeReturn = await _service.CreateAsync(request);
        return CreatedAtRoute("GetById", new {id = officeReturn.Id}, officeReturn);
    }
    
    [HttpPut("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetOfficeResponse>> Update([FromBody]EditOfficeRequest request)
    {
        var eventReturn = await _service.UpdateAsync(request);
        _publishEndpoint.Publish<IOfficeUpdated>(new
        {
            Id = eventReturn.Id,
            Address = eventReturn.Address
        });
        return Ok(eventReturn);
    }

    [HttpPut("UpdateStatus")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetOfficeResponse>> UpdateStatus([FromBody] EditOfficeStatusRequest request)
        => Ok(await _service.UpdateStatusAsync(request));
    
    
    



}