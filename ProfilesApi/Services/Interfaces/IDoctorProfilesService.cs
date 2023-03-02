using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Mail;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;

namespace ProfilesApi.Services.Interfaces;

public interface IDoctorProfilesService
{
    public Task<GetMailAndIdStuffResponse> CreateAsync(CreateDoctorProfileRequest request);
    public Task ConfirmEmailAsync(Guid accountId);
    public Task RollBackUserAsync(Guid userId);
    public Task<GetDoctorProfilesResponse> UpdateAsync(EditDoctorProfileRequest request);
    public Task<GetDoctorProfilesResponse> GetByIdAsync(Guid id);
    public Task<GetDoctorProfilesResponse> GetByUserIdAsync(Guid userId);
    public Task<bool> CheckEmailConfirmation(Guid userId);
    public Task<PageResult<GetDoctorAndPhotoProfilesResponse>> GetAllAsync(int pageNumber,int pageSize,SearchAndFilterParameters parameters);
    //public  Task<GetOfficeResponse> GetByIdAsync(Office office);
    //public Task<GetOfficeResponse> UpdateAsync(Office office,EditOfficeRequest request);
}