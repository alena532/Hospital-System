using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace DocumentsApi.DataAccess.Repositories.Interfaces;

public interface IPhotoRepository
{
    Task<ObjectId> CreateAsync(byte[] photo, string fileName);
    Task<byte[]> GetByIdAsync(ObjectId id);
}