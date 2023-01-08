using AuthApi.Contracts.Requests;
using AuthApi.Contracts.Responses;
using AuthApi.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Services.Interfaces;

public interface IAuthService
{
    Task<AuthenticatedResponse> LoginAsync(LoginRequest request);
    Task<User> RegisterAsync(RegisterRequest request);
    Task<TokensResponse> RefreshAsync(TokensRequest tokens);
    Task UpdatePasswordAsync(ChangePasswordRequest request,string username);
    Task RevokeAsync(string username);
}