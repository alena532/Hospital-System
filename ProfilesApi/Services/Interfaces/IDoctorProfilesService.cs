using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;

namespace ProfilesApi.Services.Interfaces;

public interface IDoctorProfilesService
{
    public Task<ICollection<GetDoctorProfilesResponse>> GetAllAsync(bool trackChanges);
    //public Task DeleteAsync(Office office);
    public Task CreateAsync(CreateDoctorProfileRequest request);

    public Task<ICollection<GetDoctorProfilesResponse>> GetAllByOfficeIdAsync(Guid officeId,bool trackChanges);
    //public  Task<GetOfficeResponse> GetByIdAsync(Office office);
    //public Task<GetOfficeResponse> UpdateAsync(Office office,EditOfficeRequest request);
}