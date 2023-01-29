using AutoMapper;
using ProfilesApi.Contracts.ReceptionistProfiles;
using ProfilesApi.Contracts.Requests.ReceptionistProfiles;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Mappers;

public class ReceptionistProfilesMapper:Profile
{
    public ReceptionistProfilesMapper()
    {
        CreateMap<CreateReceptionistProfileRequest, Account>();
        CreateMap<CreateReceptionistProfileRequest, Receptionist>();
        CreateMap<Receptionist, GetDetailedReceptionistProfilesResponse>();
        CreateMap<Receptionist, GetReceptionistAndPhotoProfilesResponse>();
    }
}