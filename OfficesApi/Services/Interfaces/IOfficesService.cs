using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Services.Interfaces;

public interface IOfficesService
{
    public Task<ICollection<GetOfficeResponse>> GetAllAsync();
    public Task DeleteAsync(Office office);
    public Task<GetOfficeResponse> CreateAsync(CreateOfficeRequest request);
    public  Task<GetOfficeResponse> GetByIdAsync(Office office);
    public Task<GetOfficeResponse> UpdateAsync(Office office,EditOfficeRequest request);
    public Task<GetOfficeResponse> UpdateStatusAsync(Office office, EditOfficeStatusRequest request);

}