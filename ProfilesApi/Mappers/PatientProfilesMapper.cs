using AutoMapper;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.Contracts.Responses.PatientProfiles;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Mappers;

public class PatientProfilesMapper:Profile
{
    public PatientProfilesMapper()
    {
        CreateMap<CreatePatientProfileRequest, Patient>()
            .ForMember(dest => dest.DateOfBirth,
            opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)));

        CreateMap<Patient, GetPatientProfilesResponse>();

    }
    
   
}