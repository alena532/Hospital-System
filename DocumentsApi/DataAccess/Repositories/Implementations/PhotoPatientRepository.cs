using DocumentsApi.Common;
using DocumentsApi.DataAccess.Models;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocumentsApi.DataAccess.Repositories.Implementations;

public class PhotoPatientRepository:IPhotoPatientRepository
{
    private readonly IMongoCollection<PhotoPatient> _photoPatientsCollection;
    
    public PhotoPatientRepository(IOptions<PhotoStoreDatabaseSettings> photoStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(photoStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(photoStoreDatabaseSettings.Value.DatabaseName);
        
        _photoPatientsCollection = mongoDatabase.GetCollection<PhotoPatient>(photoStoreDatabaseSettings.Value.PhotoPatientsCollectionName);
    }
    
    public async Task CreateAsync(PhotoPatient request)
    {
        await _photoPatientsCollection.InsertOneAsync(request);
    }

    public async Task<ObjectId> GetPhotoIdByPatientIdAsync(Guid patientId)
    {
        var filter = new BsonDocument { { "PatientId", patientId }};
        var photoPatient = await _photoPatientsCollection.Find(filter).SingleOrDefaultAsync();
        
        return photoPatient.PhotoId;
    }
}