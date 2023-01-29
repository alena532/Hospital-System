using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesApi.Common.Attributes;
using ServicesApi.Contracts.Requests.Services;
using ServicesApi.Contracts.Responses.Services;
using ServicesApi.Services.Interfaces;

namespace ServicesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class ServicesController : Controller
{
    private readonly IServicesService _service;
    
    
    public ServicesController(IServicesService service)
    {
        _service = service;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetServiceResponse>> Create([FromBody] CreateServiceRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }
    
    [HttpPut("UpdateStatus")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetServiceResponse>> UpdateStatus([FromBody] EditServiceStatusRequest request)
    {
        return Ok(await _service.UpdateStatusAsync(request));
    }
    
    [HttpPut]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetServiceResponse>> Update([FromBody] EditServiceRequest request)
    {
        return Ok(await _service.UpdateAsync(request));
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<GetServiceResponse>> GetById(Guid id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    
    [HttpGet("GetAllByMissingSpecialization")]
    public async Task<ActionResult<IEnumerable<GetServiceResponse>>> GetAllByMissingSpecialization()
    {
        return Ok(await _service.GetAllByMissingSpecializationAsync());
    }
    
    
    
    

}