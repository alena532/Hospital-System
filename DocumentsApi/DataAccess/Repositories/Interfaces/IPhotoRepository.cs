using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace DocumentsApi.DataAccess.Repositories.Interfaces;

public interface IPhotoRepository
{
    Task<ObjectId> CreateAsync(byte[] photo, string fileName);
    Task<string> GetFileNameByIdAsync(ObjectId id);
    public Task UpdateAsync(Photo photo, byte[] photoBytes);
    public Task DeleteAsync(ObjectId id);
    Task<byte[]> GetByIdAsync(ObjectId id);
}