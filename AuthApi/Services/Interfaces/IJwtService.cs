using System.Security.Claims;
using AuthApi.Contracts.Responses;
using AuthApi.DataAccess;

namespace AuthApi.Services.Interfaces;

public interface IJwtService
{
    AuthenticatedResponse GenerateJwtTokenAsync(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}