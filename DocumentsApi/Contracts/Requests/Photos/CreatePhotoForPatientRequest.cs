using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentsApi.Contracts.Requests.Photos;

public class CreatePhotoForPatientRequest
{
    public IFormFile Photo { get; set; }
    
    public Guid PatientId { get; set; }
}