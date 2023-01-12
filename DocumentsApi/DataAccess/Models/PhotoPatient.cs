using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentsApi.DataAccess.Models;

public class PhotoPatient
{
    [BsonId]
    public Guid Id { get; set; }
    public ObjectId PhotoId {get; set;}
    [BsonIgnore]
    public Photo PhotoReference {get; set;}
    public Guid PatientId {get; set;}
    
}