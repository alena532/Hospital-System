using ProfilesApi.Contracts.Responses.PatientProfiles;

namespace ProfilesApi.Contracts.Responses.Accounts;

public class GetAccountAndPatientProfileResponse
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public GetPatientProfilesResponse Patient { get; set; }
}