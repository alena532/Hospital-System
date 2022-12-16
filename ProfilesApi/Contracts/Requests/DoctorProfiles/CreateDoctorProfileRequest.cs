using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProfilesApi.Common;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Contracts.Requests.DoctorProfiles;

public class CreateDoctorProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }

    public Guid OfficeId { get; set; }
    public string Address { get; set; }

    public int CareerStartYear { get; set; }
    public string PhoneNumber { get; set; }
    public DoctorStatusEnum Status { get; set; }
    
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
    
    public Guid PhotoId { get; set; }
    public string Url { get; set; }
    public Guid RoleId { get; set; }
}