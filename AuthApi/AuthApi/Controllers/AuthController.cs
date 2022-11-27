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

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly IAuthService _authService;
    public AuthController(AppDbContext context, UserManager<User> userManager, IOptions<JwtOptions> jwtOptions,IAuthService authService)
    {
        _context = context;
        _userManager = userManager;
        _jwtOptions = jwtOptions?.Value ?? throw new ArgumentNullException(nameof(JwtOptions));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthenticatedResponse>> LoginAsync([FromBody] LoginRequest request)
    {
        var authResponse = await _authService.LoginAsync(request);
        return Ok(authResponse);
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        await _authService.RegisterAsync(request);
        return StatusCode(201);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Refresh")]
    public async Task<ActionResult<AuthenticatedResponse>> Refresh([FromBody] TokensRequest tokens)
    {
        var authResponse = await _authService.Refresh(tokens);
        return Ok(authResponse);
    }

    [HttpPost,Authorize]
    [Route("Revoke")]
    public async Task<ActionResult> Revoke()
    {
        await _authService.Revoke();
        return NoContent();
    }
}