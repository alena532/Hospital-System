using Microsoft.AspNetCore.Mvc;
using Orchestrator.Contracts.Requests.PatientProfiles;

namespace Orchestrator.Services.Interfaces;

public interface IDoctorProfilesService
{
    Task CreateAsync([FromForm] CreateDoctorProfileAndPhotoRequest request);
    Task UpdateAsync([FromForm] EditDoctorProfileAndPhotoRequest request);
}