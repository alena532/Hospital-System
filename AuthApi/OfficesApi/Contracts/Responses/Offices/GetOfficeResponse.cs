using OfficesApi.DataAccess.Models;

namespace OfficesApi.Contracts.Responses.Offices;

public class GetOfficeResponse
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public int Status { get; set; }
}