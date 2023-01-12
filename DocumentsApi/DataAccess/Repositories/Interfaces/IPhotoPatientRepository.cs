using DocumentsApi.DataAccess.Models;
using MongoDB.Bson;

namespace DocumentsApi.DataAccess.Repositories.Interfaces;

public interface IPhotoPatientRepository
{
    Task CreateAsync(PhotoPatient photoAccount);
}