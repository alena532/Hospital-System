using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;

namespace DocumentsApi.Services.Interfaces;

public interface IPhotosService
{ 
    Task<ObjectId> CreatePatientPhotoAsync(CreatePhotoForPatientRequest request);
    Task<ObjectId> CreateDoctorPhotoAsync(CreatePhotoForDoctorRequest request);
    Task<ObjectId> CreateReceptionistPhotoAsync(CreatePhotoForReceptionistRequest request);
    public Task UpdateByDoctorIdAsync(EditPhotoForDoctorRequest request);
    Task<byte[]> GetByPatientIdAsync(Guid patientId);
    Task<byte[]> GetByDoctorIdAsync(Guid doctorId);
    Task<byte[]> GetByReceptionistIdAsync(Guid receptionistId);
}