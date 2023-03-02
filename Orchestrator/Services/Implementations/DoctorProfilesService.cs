using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Contracts.Requests.Photo.DoctorProfiles;
using Orchestrator.Services.Interfaces;
using SharedModels.Routes;

namespace Orchestrator.Services.Implementations;

public class DoctorProfilesService:IDoctorProfilesService
{
    private readonly HttpClient _client;
    private readonly IMapper _mapper;

    public DoctorProfilesService(IMapper mapper)
    {
        _client = new HttpClient();
        _mapper = mapper;
    }
    
    public async Task<string> CreateAsync(CreateDoctorProfileAndPhotoRequest request)
    {
        var profileRequest = _mapper.Map<CreateDoctorProfileRequest>(request);
        var createdDoctor = await _client.PostAsJsonAsync(ApiRoutes.Profiles + "api/DoctorProfiles", profileRequest);
        if (createdDoctor.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdDoctor.Content} {createdDoctor.ReasonPhrase}");
        }
        
        var doctor = await createdDoctor.Content.ReadAsStringAsync();
        var dataJson = (JObject)JsonConvert.DeserializeObject(doctor);
        var doctorId = new Guid(dataJson["id"].Value<string>());
       
        if (request.Photo == null || request.Photo.Length <= 1) return null;
        
        byte[] bytes;
        using (var ms = new MemoryStream())
        {
            await request.Photo.CopyToAsync(ms);
            bytes = ms.ToArray();
        }

        var photoRequest = new CreatePhotoForDoctorProfileRequest()
        {
            Photo = bytes,
            FileName = request.Photo.FileName,
            DoctorId = doctorId
        };

        var createdPhoto = await _client.PostAsJsonAsync(ApiRoutes.Documents +"api/Photos/DoctorPhoto", photoRequest);
        if (createdPhoto.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdPhoto.Content} {createdPhoto.ReasonPhrase}");
        }

        return doctor;
    }

    public async Task UpdateAsync([FromForm] EditDoctorProfileAndPhotoRequest request)
    {
        var profileRequest = _mapper.Map<EditDoctorProfileRequest>(request);

        var updatedDoctor = await _client.PutAsJsonAsync(ApiRoutes.Profiles + "api/DoctorProfiles", profileRequest);
        if (updatedDoctor.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{updatedDoctor.Content} {updatedDoctor.ReasonPhrase}");
        }
        
        if (request.Photo == null || request.Photo.Length <= 1) return;
        
        byte[] bytes;
        using (var ms = new MemoryStream())
        {
            await request.Photo.CopyToAsync(ms);
            bytes = ms.ToArray();
        }

        var photoRequest = new EditPhotoForDoctorProfileRequest()
        {
            Photo = bytes,
            FileName = request.Photo.FileName,
            DoctorId = request.Id
        };

        var updatedPhoto = await _client.PutAsJsonAsync(ApiRoutes.Documents +"api/Photos/DoctorPhoto", photoRequest);
        if (updatedPhoto.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{updatedPhoto.Content} {updatedPhoto.ReasonPhrase}");
        }
    }
}