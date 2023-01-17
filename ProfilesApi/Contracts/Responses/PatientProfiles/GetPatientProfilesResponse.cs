using System.Text.Json.Serialization;
using ProfilesApi.Common;

namespace ProfilesApi.Contracts.Responses.PatientProfiles;

public class GetPatientProfilesResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsLinkedToAccount { get; set; }
    public Guid AccountId { get; set; }
    
}