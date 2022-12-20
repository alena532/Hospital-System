using System.Text.Json.Serialization;
using ProfilesApi.Common;

namespace ProfilesApi.Contracts.Requests.PatientProfiles;

public class CredentialsPatientProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly DateOfBirth { get; set; }
}