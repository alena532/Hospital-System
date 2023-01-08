using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using DocumentsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

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
    public async Task<ActionResult> CreatePatientPhoto([FromForm]CreatePhotoForPatientRequest request)
    {
        await _service.CreateAsync(request);
        return Ok();
    }

}