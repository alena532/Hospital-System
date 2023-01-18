using ProfilesApi.Contracts.Responses.Accounts;

namespace ProfilesApi.Services.Interfaces;

public interface IAccountsService
{
    Task CheckPatientAccountBeforeProfileCreationAsync(Guid id);
    Task<GetAccountAndPatientProfileResponse> CheckPatientAccountBeforeProfileLoginAsync(Guid id);
}