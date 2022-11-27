using AuthApi.Contracts.Requests;
using AuthApi.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Services.Interfaces;

public interface IAuthService
{
    Task<AuthenticatedResponse> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterRequest request);
    Task<AuthenticatedResponse> Refresh(TokensRequest tokens);
    Task Revoke();
}