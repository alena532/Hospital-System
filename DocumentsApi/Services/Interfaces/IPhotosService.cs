using DocumentsApi.Contracts.Requests.Photos;
using MongoDB.Bson;

namespace DocumentsApi.Services.Interfaces;

public interface IPhotosService
{ 
    Task<ObjectId> CreateAsync(CreatePhotoForPatientRequest request);
}