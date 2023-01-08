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

    public async Task<ObjectId> CreateAsync(IFormFile photo)
    {
        ObjectId id;
        using (var stream = photo.OpenReadStream())
        {
            id = await _gridFS.UploadFromStreamAsync(photo.FileName, stream);
        }

        return id;
    }
    
        

}