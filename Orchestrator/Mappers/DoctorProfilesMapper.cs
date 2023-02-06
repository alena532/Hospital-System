using AutoMapper;
using Orchestrator.Contracts.Requests.PatientProfiles;
using Orchestrator.Contracts.Requests.Photo.DoctorProfiles;

namespace Orchestrator.Mappers;

public class DoctorProfilesMapper:Profile
{
    public DoctorProfilesMapper()
    {
        CreateMap<CreateDoctorProfileAndPhotoRequest, CreateDoctorProfileRequest>();
        CreateMap<EditDoctorProfileAndPhotoRequest, EditDoctorProfileRequest>();
    }
}