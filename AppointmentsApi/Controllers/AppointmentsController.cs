using AppointmentsApi.Contracts.Requests;
using AppointmentsApi.Contracts.Responses;
using AppointmentsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AppointmentsController:ControllerBase
{
    private readonly IAppointmentsService _service;
    public AppointmentsController(IAppointmentsService service)
    {
        _service = service;
    } 
    
    [HttpPost("GetFreeTimeForAppointment")]
    public async Task<ActionResult<IEnumerable<GetFreeTimeForAppointmentResponse>>> GetFreeTimeForAppointment([FromBody]GetFreeTimeForAppointmentRequest request)
        => Ok(await _service.GetFreeTimeForAppointmentAsync(request));
    
    [HttpPost]
    public async Task<ActionResult<GetAppointmentResponse>> Create([FromBody]CreateAppointmentRequest request)
        => Ok(await _service.CreateAsync(request));
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAppointmentResponse>>> GetAll()
        => Ok(await _service.GetAllAsync());
    



}