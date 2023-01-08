using ProfilesApi.Common;
using System.Text.Json.Serialization;
namespace ProfilesApi.Contracts.Responses.PatientProfiles;

public class GetAccountUserCredentialsResponse
{
    public Guid AccountId { get; set; }
    public string ToEmail { get; set; }
}