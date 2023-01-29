using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;

namespace DocumentsApi.DataAccess.Repositories.Interfaces;

public interface IPhotoDoctorRepository
{
    Task CreateAsync(PhotoDoctor photoAccount);
    Task<ObjectId> GetPhotoIdByDoctorIdAsync(Guid doctorId);
}