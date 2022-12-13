using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Common.Attributes;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class PatientProfilesController:ControllerBase
{
    private readonly IDoctorProfilesService _service;
    
    
    public PatientProfilesController(IDoctorProfilesService service)
    {
        _service = service;
    } 
    
    //for receptionist
    
    
    
    
    

    
    
}