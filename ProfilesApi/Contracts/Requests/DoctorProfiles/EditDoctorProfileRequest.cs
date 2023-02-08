using Microsoft.AspNetCore.Mvc;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Contracts.Requests.DoctorProfiles;

public class EditDoctorProfileRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [FromForm]
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid OfficeId { get; set; }
    public string Address { get; set; }
    public int CareerStartYear { get; set; }
    public DoctorStatusEnum Status { get; set; }
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
}