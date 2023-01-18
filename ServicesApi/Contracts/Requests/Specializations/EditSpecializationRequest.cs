using ServicesApi.DataAccess.Models;

namespace ServicesApi.Contracts.Requests.Specializations;

public class EditSpecializationRequest
{
    public Guid Id { get; set; }
    public string SpecializationName { get; set; }
    public SpecializationStatus Status { get; set; }
    public List<Guid> Services { get; set; }
}