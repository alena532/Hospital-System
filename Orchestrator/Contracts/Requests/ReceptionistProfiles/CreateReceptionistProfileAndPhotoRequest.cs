using Microsoft.AspNetCore.Mvc;
using Orchestrator.DataAccess.Models;

namespace Orchestrator.Contracts.Requests.ReceptionistProfiles;

public class CreateReceptionistProfileAndPhotoRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [FromForm]
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid OfficeId { get; set; }
    public string Address { get; set; }
    [FromForm]
    public IFormFile? Photo { get; set; }
}