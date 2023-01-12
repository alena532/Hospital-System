using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.DataAccess.Models;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using DocumentsApi.Services.Interfaces;
using MongoDB.Bson;

namespace DocumentsApi.Services.Implementations;

public class PhotosService:IPhotosService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPhotoPatientRepository _photoPatientRepository;
    

    public PhotosService(IPhotoRepository photoRepository,IPhotoPatientRepository photoPatientRepository)
    {
        _photoRepository = photoRepository;
        _photoPatientRepository = photoPatientRepository;
    }
    
    public async Task<ObjectId> CreateAsync(CreatePhotoForPatientRequest request)
    {
        var photoId = await _photoRepository.CreateAsync(request.Photo,request.FileName);

        var photoPatient = new PhotoPatient()
        {
            PhotoId = photoId,
            PatientId = request.PatientId
        };
        await _photoPatientRepository.CreateAsync(photoPatient);
        return photoId;
    }

    public async Task<byte[]> GetByPatientIdAsync(Guid patientId)
    {
        var photoId = await _photoPatientRepository.GetPhotoIdByPatientIdAsync(patientId);
        if (photoId == null)
        {
            throw new BadHttpRequestException($"Photo for appropriate patient {patientId} wasn`t found");
        }

        return await _photoRepository.GetByIdAsync(photoId);
    }
}