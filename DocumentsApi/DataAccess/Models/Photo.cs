using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentsApi.DataAccess.Models;

public class Photo
{
    [BsonId]
    
    public ObjectId Id { get; set; }

    public string FileName { get; set; }
}