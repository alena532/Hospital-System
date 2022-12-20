using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;

namespace ProfilesApi.Services.Interfaces;

public interface IDoctorProfilesService
{
    //public Task DeleteAsync(Office office);
    public Task CreateAsync(CreateDoctorProfileRequest request);
    public Task ConfirmEmailAsync(Guid id);
    public Task<ICollection<GetDoctorProfilesResponse>> GetAllAsync();
    //public  Task<GetOfficeResponse> GetByIdAsync(Office office);
    //public Task<GetOfficeResponse> UpdateAsync(Office office,EditOfficeRequest request);
}