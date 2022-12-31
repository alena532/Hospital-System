using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Common.Attributes;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AccountsController:ControllerBase
{
    private readonly IAccountsService _service;
    
    public AccountsController(IAccountsService service)
    {
        _service = service;
    } 
    
    [HttpGet("GetByUserId/{userId:Guid}")]
    public async Task<ActionResult<Guid>> GetByUserId(Guid userId)
        =>Ok(await _service.GetByUserIdAsync(userId));
    
    [HttpGet("CheckAccountBeforeProfileCreation/{id:Guid}")]
    public async Task<ActionResult<Guid>> CheckAccountBeforeProfileCreation(Guid id)
        =>Ok(await _service.CheckAccountBeforeProfileCreationAsync(id));
}