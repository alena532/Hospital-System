namespace Orchestrator.Contracts.Requests.PatientProfiles;

public class PatientProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public Guid AccountId { get; set; }
    public IFormFile Photo { get; set; }
}