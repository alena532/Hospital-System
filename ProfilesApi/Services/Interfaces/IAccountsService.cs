using ProfilesApi.Contracts.Responses.Accounts;

namespace ProfilesApi.Services.Interfaces;

public interface IAccountsService
{
    Task<GetAccountResponse> GetByUserIdAsync(Guid userId);
    Task<GetAccountResponse> CheckAccountBeforeProfileCreationAsync(Guid id);
}