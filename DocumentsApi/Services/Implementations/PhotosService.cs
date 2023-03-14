using DocumentsApi.Contracts.Requests.Photos;
using DocumentsApi.Contracts.Responses;
using DocumentsApi.DataAccess.Models;
using DocumentsApi.DataAccess.Repositories.Interfaces;
using DocumentsApi.Services.Interfaces;
using MongoDB.Bson;

namespace DocumentsApi.Services.Implementations;

public class PhotosService:IPhotosService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPhotoPatientRepository _photoPatientRepository;
    private readonly IPhotoDoctorRepository _photoDoctorRepository;
    private readonly IPhotoReceptionistRepository _photoReceptionistRepository;
    
    public PhotosService(IPhotoRepository photoRepository,IPhotoPatientRepository photoPatientRepository,IPhotoDoctorRepository photoDoctorRepository,IPhotoReceptionistRepository photoReceptionistRepository)
    {
        _photoRepository = photoRepository;
        _photoPatientRepository = photoPatientRepository;
        _photoDoctorRepository = photoDoctorRepository;
        _photoReceptionistRepository = photoReceptionistRepository;
    }
    
    public async Task<ObjectId> CreatePatientPhotoAsync(CreatePhotoForPatientRequest request)
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
    
    public async Task<ObjectId> CreateDoctorPhotoAsync(CreatePhotoForDoctorRequest request)
    {
        var photoId = await _photoRepository.CreateAsync(request.Photo,request.FileName);

        var photoDoctor = new PhotoDoctor()
        {
            PhotoId = photoId,
            DoctorId = request.DoctorId
        };
        await _photoDoctorRepository.CreateAsync(photoDoctor);
        return photoId;
    }
    
    public async Task<ObjectId> CreateReceptionistPhotoAsync(CreatePhotoForReceptionistRequest request)
    {
        var photoId = await _photoRepository.CreateAsync(request.Photo,request.FileName);

        var photoReceptionist = new PhotoReceptionist()
        {
            PhotoId = photoId,
            ReceptionistId = request.ReceptionistId
        };
        await _photoReceptionistRepository.CreateAsync(photoReceptionist);
        return photoId;
    }

    public async Task<GetPhotoResponse> GetByPatientIdAsync(Guid patientId)
    {
        var photoId = await _photoPatientRepository.GetPhotoIdByPatientIdAsync(patientId);
        if (photoId == null)
        {
            throw new BadHttpRequestException($"Photo for appropriate patient {patientId} wasn`t found");
        }

        var photoResponse = new GetPhotoResponse()
        {
            Bytes = await _photoRepository.GetByIdAsync(photoId),
            FileName = await _photoRepository.GetFileNameByIdAsync(photoId)
        };

        return photoResponse;
    }
    
    public async Task<GetPhotoResponse> GetByDoctorIdAsync(Guid doctorId)
    {
        var photoId = await _photoDoctorRepository.GetPhotoIdByDoctorIdAsync(doctorId);
        if (photoId == null)
        {
            throw new BadHttpRequestException($"Photo for appropriate patient {doctorId} wasn`t found");
        }

        var photoResponse = new GetPhotoResponse()
        {
            Bytes = await _photoRepository.GetByIdAsync(photoId),
            FileName = await _photoRepository.GetFileNameByIdAsync(photoId)
        };

        return photoResponse;
    }
    
    public async Task<GetPhotoResponse> GetByReceptionistIdAsync(Guid receptionistId)
    {
        var photoId = await _photoReceptionistRepository.GetPhotoIdByReceptionistIdAsync(receptionistId);
        if (photoId == null)
        {
            throw new BadHttpRequestException($"Photo for appropriate patient {receptionistId} wasn`t found");
        }

        var Bytes = await _photoRepository.GetByIdAsync(photoId);
        var FileName = await _photoRepository.GetFileNameByIdAsync(photoId);
        var photoResponse = new GetPhotoResponse()
        {
            Bytes = await _photoRepository.GetByIdAsync(photoId),
            FileName = await _photoRepository.GetFileNameByIdAsync(photoId)
        };

        return photoResponse;
    }

    public async Task UpdateByDoctorIdAsync(EditPhotoForDoctorRequest request)
    {
        var photoId = await _photoDoctorRepository.GetPhotoIdByDoctorIdAsync(request.DoctorId);
        if (photoId == null)
        {
            throw new BadHttpRequestException($"Photo id not found");
        }
        
        await _photoRepository.DeleteAsync(photoId);
        
        Photo photo = new()
        {
            Id = photoId,
            FileName = request.FileName
        };
        await _photoRepository.UpdateAsync(photo, request.Photo);
    }
}