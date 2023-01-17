using ServicesApi.Contracts.Requests.Services;
using ServicesApi.Contracts.Requests.Specializations;
using ServicesApi.Contracts.Responses.Specializations;

namespace ServicesApi.Services.Interfaces;

public interface ISpecializationsService
{
    Task<GetSpecializationByIdResponse> CreateAsync(CreateSpecializationRequest request);
    Task<GetSpecializationByIdResponse> UpdateStatusAsync(EditSpecializationStatusRequest request);
    Task<GetSpecializationByIdResponse> UpdateAsync(EditSpecializationRequest request);
    Task<GetSpecializationByIdResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<GetSpecializationResponse>> GetAllAsync();
}