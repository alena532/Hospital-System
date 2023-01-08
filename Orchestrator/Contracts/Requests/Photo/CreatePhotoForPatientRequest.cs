namespace Orchestrator.Contracts.Requests.Photo;

public class CreatePhotoForPatientRequest
{
    public Guid PatientId { get; set; }
    public IFormFile Photo { get; set; }
}