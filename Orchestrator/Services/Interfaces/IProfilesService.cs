using Orchestrator.Contracts.Requests.PatientProfiles;

namespace Orchestrator.Services.Interfaces;

public interface IProfilesService
{
    Task CreatePatientProfileAsync(CreatePatientProfileAndPhotoRequest request);
    Task CreateDoctorProfileAsync(CreateDoctorProfileAndPhotoRequest request);
}