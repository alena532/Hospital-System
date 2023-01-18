using ServicesApi.Contracts.Requests.Services;
using ServicesApi.Contracts.Responses.Services;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.Services.Interfaces;

public interface IServicesService
{
    Task<GetServiceResponse> CreateAsync(CreateServiceRequest request);
    Task<GetServiceResponse> UpdateStatusAsync(EditServiceStatusRequest request);
    Task<GetServiceResponse> UpdateAsync(EditServiceRequest request);
    Task<GetServiceResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<GetServiceResponse>> GetAllByMissingSpecializationAsync();

}