namespace Orchestrator.Contracts.Requests.Photo.DoctorProfiles;

public class CreatePhotoForDoctorProfileRequest
{
    public Guid DoctorId { get; set; }
    public byte[] Photo { get; set; }
    public string FileName { get; set; }
}