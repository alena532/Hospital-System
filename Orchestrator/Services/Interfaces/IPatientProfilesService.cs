using Microsoft.AspNetCore.Mvc;
using Orchestrator.Contracts.Requests.PatientProfiles;

namespace Orchestrator.Services.Interfaces;

public interface IPatientProfilesService
{
    Task CreateAsync([FromForm] CreatePatientProfileAndPhotoRequest request);
}