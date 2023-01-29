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

    public DoctorProfilesService()
    {
        _client = new HttpClient();
    }
    
    public async Task CreateAsync(CreateDoctorProfileAndPhotoRequest request)
    {
        var profileRequest = new CreateDoctorProfileRequest()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
            Email = request.Email,
            OfficeId = request.OfficeId,
            Address = request.Address,
            CareerStartYear = request.CareerStartYear,
            PhoneNumber = request.PhoneNumber,
            Status = request.Status,
            SpecializationId = request.SpecializationId,
            SpecializationName = request.SpecializationName
        };
       
        var createdDoctor = await _client.PostAsJsonAsync(ApiRoutes.Profiles + "api/DoctorProfiles", profileRequest);
        if (createdDoctor.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdDoctor.Content} {createdDoctor.ReasonPhrase}");
        }
        
        var doctor = await createdDoctor.Content.ReadAsStringAsync();
        var dataJson = (JObject)JsonConvert.DeserializeObject(doctor);
        var doctorId = new Guid(dataJson["id"].Value<string>());
       
        if (request.Photo == null || request.Photo.Length <= 1) return;
        
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

        var createdPhoto = await _client.PostAsJsonAsync(ApiRoutes.Documents +"api/Photos/CreateDoctorPhoto", photoRequest);
        if (createdPhoto.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdPhoto.Content} {createdPhoto.ReasonPhrase}");
        }
    }

    public async Task UpdateAsync([FromForm] EditDoctorProfileAndPhotoRequest request)
    {
        var profileRequest = new EditDoctorProfileRequest()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
            OfficeId = request.OfficeId,
            Address = request.Address,
            CareerStartYear = request.CareerStartYear,
            Status = request.Status,
            SpecializationId = request.SpecializationId,
            SpecializationName = request.SpecializationName
        };
       
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

        var updatedPhoto = await _client.PutAsJsonAsync(ApiRoutes.Documents +"api/Photos/UpdateDoctorPhoto", photoRequest);
        if (updatedPhoto.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{updatedPhoto.Content} {updatedPhoto.ReasonPhrase}");
        }
    }
}