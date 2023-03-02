using Microsoft.AspNetCore.Mvc;
using Orchestrator.Contracts.Requests.PatientProfiles;

namespace Orchestrator.Services.Interfaces;

public interface IDoctorProfilesService
{
    Task<string> CreateAsync(CreateDoctorProfileAndPhotoRequest request);
    Task UpdateAsync(EditDoctorProfileAndPhotoRequest request);
}