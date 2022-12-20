using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthValidatorController : ControllerBase
{
    private readonly IAuthValidatorService _service;
    public AuthValidatorController(IAuthValidatorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Validate([FromBody] string Email)
    {
        await _service.ValidateEmailAsync(Email);
        return Ok();
    }
        
    
}