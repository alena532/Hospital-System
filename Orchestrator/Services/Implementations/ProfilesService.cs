using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Contracts.Requests.Photo;
using Orchestrator.Contracts.Requests.Photo.DoctorProfiles;
using Orchestrator.Services.Interfaces;
using SharedModels.Routes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Orchestrator.Services.Implementations;

public class ProfilesService:IProfilesService
{
    private readonly HttpClient _client;

    public ProfilesService()
    {
        _client = new HttpClient();
    }
    public async Task CreatePatientProfileAsync(CreatePatientProfileAndPhotoRequest request)
    {
        var profileRequest = new CreatePatientProfileRequest()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            AccountId = request.AccountId,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber
        };
        var createdPatient = await _client.PostAsJsonAsync(ApiRoutes.Profiles + "api/PatientProfiles", profileRequest);
        if (createdPatient.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdPatient.Content} {createdPatient.ReasonPhrase}");
        }
        
        var patient = await createdPatient.Content.ReadAsStringAsync();
        var dataJson = (JObject)JsonConvert.DeserializeObject(patient);
        var patientId = new Guid(dataJson["id"].Value<string>());

        if (request.Photo == null || request.Photo.Length <= 1) return;
        
        byte[] bytes;
        using (var ms = new MemoryStream())
        {
            await request.Photo.CopyToAsync(ms);
            bytes = ms.ToArray();
        }

        var photoRequest = new CreatePhotoForPatientProfileRequest()
        {
            Photo = bytes,
            FileName = request.Photo.FileName,
            PatientId = patientId
        };

        var createdPhoto = await _client.PostAsJsonAsync(ApiRoutes.Documents +"api/Photos/CreatePatientPhoto", photoRequest);
        if (createdPhoto.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdPatient.Content} {createdPatient.ReasonPhrase}");
        }
    }
    
    public async Task CreateDoctorProfileAsync(CreateDoctorProfileAndPhotoRequest request)
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
}