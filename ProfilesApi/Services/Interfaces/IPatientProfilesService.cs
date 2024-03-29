using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.PatientProfiles;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Services.Interfaces;

public interface IPatientProfilesService
{
    public Task<GetPatientProfilesResponse> CreateAsync(CreatePatientProfileRequest request);
    public Task<GetAccountUserCredentialsResponse> CreateAccountAsync(CreatePatientAccountRequest request);
    Task<GetPatientProfilesResponse> LinkPatientProfileToAccountAsync(Guid id);
    Task<ICollection<GetPatientProfilesResponse>> GetMatchesAsync(CredentialsPatientProfileRequest request);
    Task<Patient> GetByAccountIdAsync(Guid accountId);
    Task<IEnumerable<GetPatientProfilesResponse>> GetAllAsync();
}