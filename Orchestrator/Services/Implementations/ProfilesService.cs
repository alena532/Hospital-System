using System.Text.Json;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Contracts.Requests.Photo;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Services.Implementations;

public class ProfilesService:IProfilesService
{
    private readonly HttpClient _client;

    public ProfilesService()
    {
        _client = new HttpClient();
    }
    public async Task CreatePatientProfileAsync(PatientProfileRequest request)
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
        var createdPatient = await _client.PostAsJsonAsync("https://localhost:7097/api/PatientProfiles", profileRequest);
        if (createdPatient.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdPatient.Content} {createdPatient.ReasonPhrase}");
        }
        
        var patientIdStream = await createdPatient.Content.ReadAsStreamAsync();
        var patientId = JsonSerializer.Deserialize<Guid>(patientIdStream);

        if (request.Photo.Length <= 1) return;
        var photoRequest = new CreatePhotoForPatientRequest()
        {
            Photo = request.Photo,
            PatientId = patientId
        };
        
        var createdPhoto = await _client.PostAsJsonAsync("https://localhost:7034/api/Photos/Patient", photoRequest);
        if (createdPhoto.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdPatient.Content} {createdPatient.ReasonPhrase}");
        }
        

    }
}