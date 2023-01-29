using Microsoft.AspNetCore.Mvc;
using Orchestrator.Contracts.Requests.ReceptionistProfiles;

namespace Orchestrator.Services.Interfaces;

public interface IReceptionistProfilesService
{
    Task CreateAsync([FromForm] CreateReceptionistProfileAndPhotoRequest request);
}