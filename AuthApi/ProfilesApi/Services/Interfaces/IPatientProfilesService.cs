using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.PatientProfiles;

namespace ProfilesApi.Services.Interfaces;

public interface IPatientProfilesService
{
    public Task CreateAsync(CreatePatientProfileRequest request);
    Task<List<GetPatientProfilesResponse>> GetMatchesAsync(Guid id);
}