namespace Orchestrator.Contracts.Requests.Photo.ReceptionistProfiles;

public class CreatePhotoForReceptionistProfileRequest
{
    public Guid ReceptionistId { get; set; }
    public byte[] Photo { get; set; }
    public string FileName { get; set; }
}