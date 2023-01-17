namespace DocumentsApi.Contracts.Requests.Photos;

public class CreatePhotoForDoctorRequest
{
    public byte[] Photo { get; set; }
    public string FileName { get; set; }
    public Guid DoctorId { get; set; }
}