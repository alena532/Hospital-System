using Orchestrator.Contracts.Requests.PatientProfiles;

namespace Orchestrator.Services.Interfaces;

public interface IProfilesService
{
    Task CreatePatientProfileAsync(PatientProfileRequest request);
}