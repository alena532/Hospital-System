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
    
    [HttpPost("PatientPhoto")]
    public async Task<ActionResult> CreatePatientPhoto(CreatePhotoForPatientRequest request)
    {
        await _service.CreatePatientPhotoAsync(request);
        return Ok();
    }
    
    [HttpGet("PatientPhoto/{patientId:Guid}")]
    public async Task<ActionResult<byte []>> GetPatientPhotoByPatientId(Guid patientId)
    {
        return Ok(await _service.GetByPatientIdAsync(patientId));
    }
    
    [HttpPost("DoctorPhoto")]
    public async Task<ActionResult> CreateDoctorPhoto(CreatePhotoForDoctorRequest request)
    {
        await _service.CreateDoctorPhotoAsync(request);
        return Ok();
    }
    
    [HttpGet("DoctorPhoto/{doctorId:Guid}")]
    public async Task<ActionResult<byte []>> GetDoctorPhotoByDoctorId(Guid doctorId)
    {
        return Ok(await _service.GetByDoctorIdAsync(doctorId));
    }
    
    [HttpPut("DoctorPhoto")]
    public async Task<ActionResult> UpdateDoctorPhotoByDoctorId(EditPhotoForDoctorRequest request)
    {
        await _service.UpdateByDoctorIdAsync(request);
        return Ok();
    }

    [HttpPost("ReceptionistPhoto")]
    public async Task<ActionResult> CreateReceptionistPhoto(CreatePhotoForReceptionistRequest request)
    {
        await _service.CreateReceptionistPhotoAsync(request);
        return Ok();
    }

    [HttpGet("ReceptionistPhoto/{receptionistId:Guid}")]
    public async Task<ActionResult<byte []>> GetDoctorPhotoByReceptionistId(Guid receptionistId)
    {
        return Ok(await _service.GetByReceptionistIdAsync(receptionistId));
    }

}