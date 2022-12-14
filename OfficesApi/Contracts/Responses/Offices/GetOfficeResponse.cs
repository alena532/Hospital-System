using OfficesApi.DataAccess.Models;

namespace OfficesApi.Contracts.Responses.Offices;

public class GetOfficeResponse
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public OfficeStatus Status { get; set; }
}