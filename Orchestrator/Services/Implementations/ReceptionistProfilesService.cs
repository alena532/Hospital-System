using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchestrator.Contracts.Requests.Photo.ReceptionistProfiles;
using Orchestrator.Contracts.Requests.ReceptionistProfiles;
using Orchestrator.Services.Interfaces;
using SharedModels.Routes;

namespace Orchestrator.Services.Implementations;

public class ReceptionistProfilesService:IReceptionistProfilesService
{
    private readonly HttpClient _client;
    private readonly IMapper _mapper;

    public ReceptionistProfilesService(IMapper mapper)
    {
        _client = new HttpClient();
        _mapper = mapper;
    }
    
    public async Task<string> CreateAsync(CreateReceptionistProfileAndPhotoRequest request)
    {
        var profileRequest = _mapper.Map<CreateReceptionistProfileRequest>(request);

        var createdReceptionist = await _client.PostAsJsonAsync(ApiRoutes.Profiles + "api/ReceptionistProfiles", profileRequest);
        if (createdReceptionist.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdReceptionist.Content} {createdReceptionist.ReasonPhrase}");
        }
        
        var receptionist = await createdReceptionist.Content.ReadAsStringAsync();
        var dataJson = (JObject)JsonConvert.DeserializeObject(receptionist);
        var receptionistId = new Guid(dataJson["id"].Value<string>());
       
        if (request.Photo == null || request.Photo.Length <= 1) return null;
        
        byte[] bytes;
        using (var ms = new MemoryStream())
        {
            await request.Photo.CopyToAsync(ms);
            bytes = ms.ToArray();
        }

        var photoRequest = new CreatePhotoForReceptionistProfileRequest()
        {
            Photo = bytes,
            FileName = request.Photo.FileName,
            ReceptionistId = receptionistId
        };

        var createdPhoto = await _client.PostAsJsonAsync(ApiRoutes.Documents +"api/Photos/ReceptionistPhoto", photoRequest);
        if (createdPhoto.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdPhoto.Content} {createdPhoto.ReasonPhrase}");
        }

        return receptionist;
    }
}