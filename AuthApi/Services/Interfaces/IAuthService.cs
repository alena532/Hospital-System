using AuthApi.Contracts.Requests;
using AuthApi.Contracts.Responses;
using AuthApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Services.Interfaces;

public interface IAuthService
{
    Task<AuthenticatedResponse> LoginAsync(LoginRequest request);
    Task<User> RegisterAsync(RegisterRequest request);
    Task<AuthenticatedResponse> RefreshAsync(AuthenticatedResponse tokens);
    Task UpdatePasswordAsync(ChangePasswordRequest request);
    Task RevokeAsync();
}