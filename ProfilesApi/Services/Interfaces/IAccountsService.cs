using ProfilesApi.Contracts.Responses.Accounts;
using ProfilesApi.Contracts.Responses.PatientProfiles;

namespace ProfilesApi.Services.Interfaces;

public interface IAccountsService
{
    Task<GetAccountResponse> GetByUserIdAsync(Guid userId);
    Task CheckAccountBeforeProfileCreationAsync(Guid id);
    Task<GetAccountResponse> CheckAccountBeforeProfileLoginAsync(Guid id);
}