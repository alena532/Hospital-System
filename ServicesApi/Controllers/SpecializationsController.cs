using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesApi.Common.Attributes;
using ServicesApi.Contracts.Requests.Services;
using ServicesApi.Contracts.Requests.Specializations;
using ServicesApi.Contracts.Responses.Services;
using ServicesApi.Contracts.Responses.Specializations;
using ServicesApi.Services.Interfaces;

namespace ServicesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class SpecializationsController : Controller
{
    private readonly ISpecializationsService _service;
    
    
    public SpecializationsController(ISpecializationsService service)
    {
        _service = service;
        
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetSpecializationResponse>> Create([FromBody] CreateSpecializationRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }
    
    [HttpPut("UpdateStatus")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetSpecializationByIdResponse>> UpdateStatus([FromBody] EditSpecializationStatusRequest request)
    {
        return Ok(await _service.UpdateStatusAsync(request));
    }
    
    [HttpPut]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<GetSpecializationByIdResponse>> Update([FromBody] EditSpecializationRequest request)
    {
        return Ok(await _service.UpdateAsync(request));
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<GetSpecializationByIdResponse>> GetById(Guid id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetSpecializationByIdResponse>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    
    
    
    
    

}