using System.Security.Claims;
using AuthApi.Common.Attributes;
using AuthApi.Contracts.Requests;
using AuthApi.Contracts.Responses;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceExtensions.Attributes;

namespace AuthApi.Controllers;


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
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<AuthenticatedResponse>> Login([FromBody] LoginRequest request)
    {
        var authResponse = await _authService.LoginAsync(request);
        return Ok(authResponse);
    }
    
    
    [HttpPost("Register")]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterRequest request)
    {
        var user = await _authService.RegisterAsync(request);
        return Ok(user.Id);
    }
    
    [HttpPost]
    [Route("Refresh")]
    [Authorize]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<TokensResponse>> Refresh([FromBody] TokensRequest tokens)
    {
        var tokenResponse = await _authService.RefreshAsync(tokens);
        return Ok(tokenResponse);
    }
    
    [HttpPut]
    [Route("ChangePassword")]
    [Authorize]
    [ServiceFilter(typeof(ValidationModelAttribute))]
    public async Task<ActionResult<AuthenticatedResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
        await _authService.UpdatePasswordAsync(request,username);
        return Ok();
    }
    
    [HttpPost]
    [Authorize]
    [Route("Revoke")]
    public async Task<ActionResult> Revoke()
    {
        var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
        await _authService.RevokeAsync(username);
        return Ok();
    }
    
}