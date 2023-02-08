using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;

namespace DocumentsApi.DataAccess.Repositories.Interfaces;

public interface IPhotoReceptionistRepository
{
    Task CreateAsync(PhotoReceptionist photoAccount);
    Task<ObjectId> GetPhotoIdByReceptionistIdAsync(Guid receptionistId);
}