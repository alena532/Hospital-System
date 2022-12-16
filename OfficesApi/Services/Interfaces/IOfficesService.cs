using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Services.Interfaces;

public interface IOfficesService
{
    public Task<ICollection<GetOfficeResponse>> GetAllAsync();
    public Task DeleteAsync(Guid id);
    public Task<GetOfficeResponse> CreateAsync(CreateOfficeRequest request);
    public  Task<GetOfficeResponse> GetByIdAsync(Guid id);
    public Task<GetOfficeResponse> UpdateAsync(EditOfficeRequest request);
    public Task<GetOfficeResponse> UpdateStatusAsync(EditOfficeStatusRequest request);

}