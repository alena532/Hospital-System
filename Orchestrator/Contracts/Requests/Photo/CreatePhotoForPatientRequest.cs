namespace Orchestrator.Contracts.Requests.Photo;

public class CreatePhotoForPatientRequest
{
    public Guid PatientId { get; set; }
    public byte[] Photo { get; set; }
    public string FileName { get; set; }
}