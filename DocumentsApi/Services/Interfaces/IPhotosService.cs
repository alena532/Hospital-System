using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.Contracts.Responses;
using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;

namespace DocumentsApi.Services.Interfaces;

public interface IPhotosService
{ 
    Task<ObjectId> CreatePatientPhotoAsync(CreatePhotoForPatientRequest request);
    Task<ObjectId> CreateDoctorPhotoAsync(CreatePhotoForDoctorRequest request);
    Task<ObjectId> CreateReceptionistPhotoAsync(CreatePhotoForReceptionistRequest request);
    public Task UpdateByDoctorIdAsync(EditPhotoForDoctorRequest request);
    Task<GetPhotoResponse> GetByPatientIdAsync(Guid patientId);
    Task<GetPhotoResponse> GetByDoctorIdAsync(Guid doctorId);
    Task<GetPhotoResponse> GetByReceptionistIdAsync(Guid receptionistId);
}