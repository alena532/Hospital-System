using ServicesApi.DataAccess.Models;

namespace ServicesApi.Contracts.Requests.Services;

public class EditServiceStatusRequest
{
    public Guid Id { get; set; }
    public ServiceStatus Status { get; set; }
}