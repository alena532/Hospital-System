using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;

namespace DocumentsApi.Services.Interfaces;

public interface IPhotosService
{ 
    Task<ObjectId> CreateAsync(CreatePhotoForPatientRequest request);
    Task<byte[]> GetByPatientIdAsync(Guid patientId);
}