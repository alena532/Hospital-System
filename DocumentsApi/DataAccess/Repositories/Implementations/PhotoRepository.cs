using DocumentsApi.Common;
using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.DataAccess.Models;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DocumentsApi.DataAccess.Repositories.Implementations;

public class PhotoRepository:IPhotoRepository
{
    private readonly IMongoCollection<Photo> _photosCollection;
    private readonly IGridFSBucket _gridFS;
    
    public PhotoRepository(IOptions<PhotoStoreDatabaseSettings> photoStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(photoStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(photoStoreDatabaseSettings.Value.DatabaseName);

        _gridFS = new GridFSBucket(mongoDatabase);

        _photosCollection = mongoDatabase.GetCollection<Photo>(photoStoreDatabaseSettings.Value.PhotosCollectionName);
    }

    public async Task<ObjectId> CreateAsync(byte[] photo,string fileName)
    {
        ObjectId id;
        id = await _gridFS.UploadFromBytesAsync(fileName, photo);

        return id;
    }

    public async Task<string> GetFileNameByIdAsync(ObjectId id)
    {
        
         var f =    new BsonDocument { { "_id", id }};
        var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Id, id);
        var fileInfos = await _gridFS.FindAsync(f);
        var fileInfo = await fileInfos.FirstOrDefaultAsync();
        return fileInfo.Filename;
    }

    public async Task<byte[]> GetByIdAsync(ObjectId id)
    {
        byte[] bytes;
        using (var ms = new MemoryStream())
        {
            await _gridFS.DownloadToStreamAsync(id, ms);
            bytes = ms.ToArray();
        }

        return bytes;
    }
    
    public async Task DeleteAsync(ObjectId id)
    {
        await _gridFS.DeleteAsync(id);
    }
    
    public async Task UpdateAsync(Photo photo,byte[] photoBytes)
    {
        await _gridFS.UploadFromBytesAsync(photo.Id, photo.FileName, photoBytes);
    }
        

}