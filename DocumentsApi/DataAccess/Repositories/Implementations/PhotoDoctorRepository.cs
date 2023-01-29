using DocumentsApi.Common;
using DocumentsApi.DataAccess.Models;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocumentsApi.DataAccess.Repositories.Implementations;

public class PhotoDoctorRepository:IPhotoDoctorRepository
{
    private readonly IMongoCollection<PhotoDoctor> _photoDoctorsCollection;
    
    public PhotoDoctorRepository(IOptions<PhotoStoreDatabaseSettings> photoStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(photoStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(photoStoreDatabaseSettings.Value.DatabaseName);

        _photoDoctorsCollection = mongoDatabase.GetCollection<PhotoDoctor>(photoStoreDatabaseSettings.Value.PhotoDoctorsCollectionName);
    }
    
    public async Task CreateAsync(PhotoDoctor request)
    {
        await _photoDoctorsCollection.InsertOneAsync(request);
    }

    public async Task<ObjectId> GetPhotoIdByDoctorIdAsync(Guid doctorId)
    {
        var filter = new BsonDocument { { "DoctorId", doctorId }};
        var photoDoctor = await _photoDoctorsCollection.Find(filter).SingleOrDefaultAsync();
        if (photoDoctor == null || photoDoctor.PhotoId == null)
        {
            throw new BadHttpRequestException("Photo for doctor not found");
        }
        return photoDoctor.PhotoId;
    }
    
    
}