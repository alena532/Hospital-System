using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Common.Attributes;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Contracts.Requests.ReceptionistProfiles;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class ReceptionistProfilesController : Controller
{
    private readonly IReceptionistProfilesService _service;
    
    public ReceptionistProfilesController(IReceptionistProfilesService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles="Receptionist")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult> Create([FromForm]CreateReceptionistProfileAndPhotoRequest request)
    {
        await _service.CreateAsync(request);
        return Ok();
    }
       
    

}