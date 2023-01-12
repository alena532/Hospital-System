using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Encodings;
using ProfilesApi.Common.Attributes;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.Accounts;
using ProfilesApi.Contracts.Responses.PatientProfiles;
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
    public async Task<ActionResult> CheckAccountBeforeProfileCreation(Guid id)
    {
        await _service.CheckAccountBeforeProfileCreationAsync(id);
        return Ok();
    }
    
    
    [HttpGet("CheckAccountBeforeProfileLogin/{userId:Guid}")]
    public async Task<ActionResult<GetPatientProfilesResponse>> CheckAccountBeforeProfileLogin(Guid userId)
        =>Ok(await _service.CheckAccountBeforeProfileLoginAsync(userId));
}