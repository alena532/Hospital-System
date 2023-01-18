using ServicesApi.DataAccess.Models;

namespace ServicesApi.Contracts.Requests.Specializations;

public class EditSpecializationStatusRequest
{
    public Guid Id { get; set; }
    public SpecializationStatus Status { get; set; }
}