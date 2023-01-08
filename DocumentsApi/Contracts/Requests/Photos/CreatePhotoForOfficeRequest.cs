namespace DocumentsApi.Contracts.Requests.Photos;

public class CreatePhotoForOfficeRequest
{
    public IFormFile Photo { get; set; }
    public Guid OfficeId { get; set; }
}