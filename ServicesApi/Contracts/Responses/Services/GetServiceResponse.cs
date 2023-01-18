using ServicesApi.DataAccess.Models;

namespace ServicesApi.Contracts.Responses.Services;

public class GetServiceResponse
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public int Price { get; set; }
    public ServiceStatus Status { get; set; }
    public string ServiceCategoryName { get; set; }
}