using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Contracts.Requests.Photo;
using Orchestrator.Services.Interfaces;
using SharedModels.Routes;

namespace Orchestrator.Services.Implementations;

public class PatientProfilesService:IPatientProfilesService
{
    private readonly HttpClient _client;
    private readonly IMapper _mapper;

    public PatientProfilesService(IMapper mapper)
    {
        _client = new HttpClient();
        _mapper = mapper;
    }
    
    public async Task CreateAsync(CreatePatientProfileAndPhotoRequest request)
    {
        var profileRequest = _mapper.Map<CreatePatientProfileRequest>(request);
        
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
}