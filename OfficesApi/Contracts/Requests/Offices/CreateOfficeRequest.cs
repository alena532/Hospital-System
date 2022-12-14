using OfficesApi.DataAccess.Models;

namespace OfficesApi.Contracts.Requests.Offices;

public class CreateOfficeRequest
{
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public OfficeStatus Status { get; set; }
}