using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DocumentsApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotosService _service;
    
    public PhotosController(IPhotosService service)
    {
        _service = service;
    }
    
    
    [HttpPost("Patient")]
    public async Task<ActionResult> CreatePatientPhoto(CreatePhotoForPatientRequest request)
    {
        await _service.CreateAsync(request);
        return Ok();
    }
    
    [HttpGet("{patientId:Guid}")]
    public async Task<ActionResult<byte []>> GetPatientPhotoByPatientId(Guid patientId)
    {
        return Ok(await _service.GetByPatientIdAsync(patientId));
    }

}