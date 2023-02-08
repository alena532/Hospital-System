

namespace DocumentsApi.Contracts.Requests.Photos;

public class CreatePhotoForPatientRequest
{
    public byte[] Photo { get; set; }
    public string FileName { get; set; }
    public Guid PatientId { get; set; }
}