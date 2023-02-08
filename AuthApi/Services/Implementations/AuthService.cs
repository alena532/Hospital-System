using System.Security.Claims;
using System.Web;
using AuthApi.Common.Attributes;
using AuthApi.Contracts.Requests;
using AuthApi.Contracts.Responses;
using AuthApi.DataAccess;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AuthApi.Services.Implementations;

public class AuthService:IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IAuthValidatorService _validatorService;

    public AuthService(UserManager<User> userManager,AppDbContext context,IJwtService jwtService,IAuthValidatorService validatorService)
    {
        _userManager = userManager;
        _context = context;
        _jwtService = jwtService;
        _validatorService = validatorService;
    }

    public async Task<AuthenticatedResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new BadHttpRequestException("Invalid email and (or) password");
        }
        user = _context.Users.Where(x => x.Id == user.Id).Include(x => x.Role).FirstOrDefault();

        var tokensResponse = _jwtService.GenerateJwtToken(user);
        if (tokensResponse == null)
        {
            throw new BadHttpRequestException("Invalid attempt");
        }

        user.RefreshToken = tokensResponse.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        _context.SaveChanges();

        var authResponse = new AuthenticatedResponse()
        {
            Tokens = tokensResponse,
            User = new UserCredentialsResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.Name
            }
        };

        return authResponse;
    }
    
    public async Task<User> RegisterAsync(RegisterRequest request)
    {
        var role = await _context.Roles.FindAsync(request.RoleId);
        if (role == null)
        {
            throw new BadHttpRequestException("Role not found");
        }
        
        await _validatorService.ValidateEmailAsync(request.Email);

        var newUser = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Role = role
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException(string.Join("\n", result.Errors));
        }

        return newUser;
    }
    
    public async Task<TokensResponse> RefreshAsync(TokensRequest tokens)
    {
        string accessToken = tokens.Token;
        string refreshToken = tokens.RefreshToken;
        
        var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
        
        var username = principal.Identity?.Name;
        var user = await _context.Users.Where(x => x.UserName == username).Include(x=>x.Role).FirstOrDefaultAsync();
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new BadHttpRequestException("Invalid user");
        }

        var tokensResponse = _jwtService.GenerateJwtToken(user);
        if (tokensResponse == null)
        {
            throw new BadHttpRequestException("Invalid attempt");
        }

        user.RefreshToken = tokensResponse.RefreshToken;
        _context.SaveChangesAsync();

        return tokensResponse;
    }
    
    public async Task RevokeAsync(string username)
    {
        var user = await _context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new BadHttpRequestException("Invalid user");
        }
        user.RefreshToken = null;
        _context.SaveChangesAsync();
    }

    
    public async Task UpdatePasswordAsync(ChangePasswordRequest request,string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
        {
            throw new BadHttpRequestException("Invalid email and (or) password");
        }

        var result = _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Result.Succeeded)
        {
            throw new BadHttpRequestException(string.Join("\n", result.Exception));
        }
    }
    
}