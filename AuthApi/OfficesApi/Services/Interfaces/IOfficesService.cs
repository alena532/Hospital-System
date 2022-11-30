using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;

namespace OfficesApi.Services.Interfaces;

public interface IOfficesService
{
    public Task<ICollection<GetOfficeResponse>> GetAllAsync();
    public Task DeleteAsync(int id);
    public Task<GetOfficeResponse> CreateAsync(CreateOfficeRequest request);
    public  Task<GetOfficeResponse> GetByIdAsync(int id);
    public Task<GetOfficeResponse> UpdateAsync(int id,EditOfficeRequest request);
    
}