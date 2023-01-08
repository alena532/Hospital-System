using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.DataAccess.Models;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using DocumentsApi.Services.Interfaces;
using MongoDB.Bson;

namespace DocumentsApi.Services.Implementations;

public class PhotosService:IPhotosService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPhotoPatientRepository _photoAccountRepository;
    

    public PhotosService(IPhotoRepository photoRepository,IPhotoPatientRepository photoAccountRepository)
    {
        _photoRepository = photoRepository;
        _photoAccountRepository = photoAccountRepository;
    }
    
    public async Task<ObjectId> CreateAsync(CreatePhotoForPatientRequest request)
    {
        var photoId = await _photoRepository.CreateAsync(request.Photo);

        var photoAccount = new PhotoPatient()
        {
            PhotoId = photoId,
            PatientId = request.PatientId
        };
        await _photoAccountRepository.CreateAsync(photoAccount);
        return photoId;
    }
}