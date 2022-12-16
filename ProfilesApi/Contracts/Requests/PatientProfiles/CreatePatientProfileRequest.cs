using System.Text.Json.Serialization;
using ProfilesApi.Common;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Contracts.Requests.PatientProfiles;

public class CreatePatientProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public Guid AccountId { get; set; }
    
    public string Url { get; set; }
}