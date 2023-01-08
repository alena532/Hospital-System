using System.ComponentModel.DataAnnotations;

namespace ProfilesApi.Contracts.Requests.PatientProfiles;

public class CreatePatientAccountRequest
{
    [EmailAddress,Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public Guid RoleId { get; set; }
}