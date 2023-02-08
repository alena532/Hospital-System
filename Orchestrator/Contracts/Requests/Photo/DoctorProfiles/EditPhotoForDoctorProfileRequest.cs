namespace Orchestrator.Contracts.Requests.Photo.DoctorProfiles;

public class EditPhotoForDoctorProfileRequest
{
    public Guid DoctorId { get; set; }
    public byte[] Photo { get; set; }
    public string FileName { get; set; }
}