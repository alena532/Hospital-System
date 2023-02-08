namespace DocumentsApi.Contracts.Requests.Photos;

public class CreatePhotoForReceptionistRequest
{
    public byte[] Photo { get; set; }
    public string FileName { get; set; }
    public Guid ReceptionistId { get; set; }
}