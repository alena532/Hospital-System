using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;

namespace ProfilesApi.Services.Interfaces;

public interface IDoctorProfilesService
{
    public Task CreateAsync(CreateDoctorProfileRequest request);
    public Task ConfirmEmailAsync(Guid id);
    public Task<PageResult<GetDoctorProfilesResponse>> GetAllAsync(int pageNumber,int pageSize,SearchAndFilterParameters parameters);
    //public  Task<GetOfficeResponse> GetByIdAsync(Office office);
    //public Task<GetOfficeResponse> UpdateAsync(Office office,EditOfficeRequest request);
}