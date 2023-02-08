using DocumentsApi.Common;
using DocumentsApi.DataAccess.Models;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocumentsApi.DataAccess.Repositories.Implementations;

public class PhotoReceptionistRepository:IPhotoReceptionistRepository
{
    private readonly IMongoCollection<PhotoReceptionist> _photoReceptionistsCollection;
    
    public PhotoReceptionistRepository(IOptions<PhotoStoreDatabaseSettings> photoStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(photoStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(photoStoreDatabaseSettings.Value.DatabaseName);
        _photoReceptionistsCollection = mongoDatabase.GetCollection<PhotoReceptionist>(photoStoreDatabaseSettings.Value.PhotoReceptionistsCollectionName);
        
    }
    
    public async Task CreateAsync(PhotoReceptionist request)
    {
        await _photoReceptionistsCollection.InsertOneAsync(request);
    }

    public async Task<ObjectId> GetPhotoIdByReceptionistIdAsync(Guid receptionistId)
    {
        var filter = new BsonDocument { { "ReceptionistId", receptionistId }};
        var photoReceptionist = await _photoReceptionistsCollection.Find(filter).SingleOrDefaultAsync();
        
        return photoReceptionist.PhotoId;
    }
}