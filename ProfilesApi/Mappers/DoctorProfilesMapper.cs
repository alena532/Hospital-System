using AutoMapper;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Mappers;

public class DoctorProfilesMapper:Profile
{
    public DoctorProfilesMapper()
    {
        CreateMap<CreateDoctorProfileRequest, Doctor>();
        CreateMap<Doctor, GetDoctorProfilesResponse>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}{src.MiddleName}"))
            .ForMember(dest => dest.Experience,
            opt => opt.MapFrom(src => src.CareerStartYear + 1));
        
    }
    
   
}