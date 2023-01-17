using ServicesApi.DataAccess.Models;

namespace ServicesApi.Contracts.Responses.Specializations;

public class GetSpecializationResponse
{
    public Guid Id { get; set; }
    public string SpecializationName { get; set; }
    public SpecializationStatus Status { get; set; }
}