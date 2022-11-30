using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficesApi.Common.Attributes;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.Services.Interfaces;

namespace OfficesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class EventsController:ControllerBase
{
    private readonly IOfficesService _officesService;
    
    public EventsController(IOfficesService officesService)
    {
        _officesService = officesService;
        
    } 
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetOfficeResponse>>> GetAllAsync()
    {
       return Ok(await _officesService.GetAllAsync());
    }

    [HttpGet("{id:int}",Name = "GetById")]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> GetByIdAsync(int id)
        =>Ok(await _officesService.GetByIdAsync(id));
    
    
    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _officesService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> CreateAsync([FromBody] CreateOfficeRequest request)
    {
        var officeReturn = await _officesService.CreateAsync(request);
        return CreatedAtRoute("GetById", new {id = officeReturn.Id}, officeReturn);
    }
    

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    [ServiceFilter(typeof(ValidationOfficeExistsAttribute))]
    
    public async Task<ActionResult<GetOfficeResponse>> UpdateAsync(int id, [FromBody]EditOfficeRequest request)
    {
        var eventReturn = await _officesService.UpdateAsync(id, request);
        return Ok(eventReturn);
    }
    
}