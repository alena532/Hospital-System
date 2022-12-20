using AuthApi.Common.Attributes;
using AuthApi.ConfigurationOptions;
using AuthApi.Contracts.Requests;
using AuthApi.Contracts.Responses;
using AuthApi.DataAccess;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("Login")]
    [ValidationModel]
    public async Task<ActionResult<AuthenticatedResponse>> Login([FromBody] LoginRequest request)
    {
        var authResponse = await _authService.LoginAsync(request);
        return Ok(authResponse);
    }
    
    [AllowAnonymous]
    [HttpPost("Register")]
    [ValidationModel]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterRequest request)
    {
        var user = await _authService.RegisterAsync(request);
        return Ok(user.Id);
    }
    
    [HttpPost]
    [Route("Refresh")]
    [ValidationModel]
    public async Task<ActionResult<AuthenticatedResponse>> Refresh([FromBody] TokensRequest tokens)
    {
        var authResponse = await _authService.RefreshAsync(tokens);
        return Ok(authResponse);
    }
    
    [HttpPut]
    [Authorize]
    [ValidationModel]
    public async Task<ActionResult<AuthenticatedResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await _authService.UpdatePasswordAsync(request);
        return StatusCode(201);
    }

    [HttpPost]
    [Authorize]
    [Route("Revoke")]
    public async Task<ActionResult> Revoke()
    {
        await _authService.RevokeAsync();
        return NoContent();
    }
}