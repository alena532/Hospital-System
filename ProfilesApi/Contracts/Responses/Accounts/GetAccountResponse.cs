using ProfilesApi.Contracts.Responses.PatientProfiles;

namespace ProfilesApi.Contracts.Responses.Accounts;

public class GetAccountResponse
{
    public string Email { get; set; }
    public GetPatientProfilesResponse Patient { get; set; }
}