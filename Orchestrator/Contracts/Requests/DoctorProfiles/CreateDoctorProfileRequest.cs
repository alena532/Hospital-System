using Orchestrator.DataAccess.Models;

namespace Orchestrator.Contracts.Requests.PatientProfiles;

public class CreateDoctorProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public Guid OfficeId { get; set; }
    public string Address { get; set; }
    public DateTime CareerStartYear { get; set; }
    public string PhoneNumber { get; set; }
    public DoctorStatusEnum Status { get; set; }
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
    
}