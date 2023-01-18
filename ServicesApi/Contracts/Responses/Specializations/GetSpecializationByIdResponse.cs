using ServicesApi.Contracts.Responses.Services;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.Contracts.Responses.Specializations;

public class GetSpecializationByIdResponse
{
    public Guid Id { get; set; }
    public string SpecializationName { get; set; }
    public SpecializationStatus Status { get; set; }
    public List<GetServiceResponse> Services { get; set; }
}