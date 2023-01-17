using System.ComponentModel.DataAnnotations;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.Contracts.Requests.Services;

public class EditServiceRequest
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    [Range(1, int.MaxValue)]
    public int Price { get; set; }
    public ServiceStatus Status { get; set; }
    public Guid ServiceCategoryId { get; set; }
}