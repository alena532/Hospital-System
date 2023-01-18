using Microsoft.AspNetCore.Mvc;

namespace Orchestrator.Contracts.Requests.PatientProfiles;

public class CreatePatientProfileAndPhotoRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [FromForm]
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public Guid AccountId { get; set; }
    [FromForm]
    public IFormFile? Photo { get; set; }
}