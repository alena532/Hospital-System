using AuthApi.Contracts.Requests;
using AuthApi.Contracts.Responses;
using AuthApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Services.Interfaces;

public interface IAuthService
{
    Task<AuthenticatedResponse> LoginAsync(LoginRequest request);
    Task<User> RegisterAsync(RegisterRequest request);
    Task<AuthenticatedResponse> Refresh(TokensRequest tokens);
    Task UpdatePassword(ChangePasswordRequest request);
    Task Revoke();
}