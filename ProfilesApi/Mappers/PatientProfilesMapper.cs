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
        CreateMap<CreatePatientProfileRequest, Patient>();
        CreateMap<Patient, GetPatientProfilesResponse>();

    }
    
   
}