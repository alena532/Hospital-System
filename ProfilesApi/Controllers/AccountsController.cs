using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("CheckPatientAccountBeforeProfileCreation/{id:Guid}")]
    public async Task<ActionResult> CheckPatientAccountBeforeProfileCreation(Guid id)
    {
        await _service.CheckPatientAccountBeforeProfileCreationAsync(id);
        return Ok();
    }

    [HttpGet("CheckPatientAccountBeforeProfileLogin/{userId:Guid}")]
    public async Task<ActionResult<GetAccountAndPatientProfileResponse>> CheckPatientAccountBeforeProfileLogin(Guid userId)
        =>Ok(await _service.CheckPatientAccountBeforeProfileLoginAsync(userId));
}