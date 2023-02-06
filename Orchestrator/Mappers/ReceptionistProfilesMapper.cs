using AutoMapper;
using Orchestrator.Contracts.Requests.Photo.ReceptionistProfiles;
using Orchestrator.Contracts.Requests.ReceptionistProfiles;

namespace Orchestrator.Mappers;

public class ReceptionistProfilesMapper:Profile
{
    public ReceptionistProfilesMapper()
    {
        CreateMap<CreateReceptionistProfileAndPhotoRequest, CreateReceptionistProfileRequest>();
       
    }
}