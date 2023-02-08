using AutoMapper;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Contracts.Requests.Photo;
using Orchestrator.Contracts.Requests.Photo.DoctorProfiles;

namespace Orchestrator.Mappers;

public class PatientProfilesMapper:Profile
{
    public PatientProfilesMapper()
    {
        CreateMap<CreatePatientProfileAndPhotoRequest, CreatePatientProfileRequest>();
    }
}