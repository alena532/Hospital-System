using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Mail;
using ProfilesApi.Contracts.ReceptionistProfiles;
using ProfilesApi.Contracts.Requests.ReceptionistProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;

namespace ProfilesApi.Services.Interfaces;

public interface IReceptionistProfilesService
{
    public Task<GetMailAndIdStuffResponse> CreateAsync(CreateReceptionistProfileRequest request);
    public Task DeleteAsync(Guid id);
    public Task<GetDetailedReceptionistProfilesResponse> GetByUserIdAsync(Guid userId);
    public Task<GetDetailedReceptionistProfilesResponse> GetByIdAsync(Guid id);
    public Task<PageResult<GetReceptionistAndPhotoProfilesResponse>> GetPageAsync(int pageNumber, int pageSize);
    public Task<PageResult<GetReceptionistAndPhotoProfilesResponse>> GetAllAsync();
}