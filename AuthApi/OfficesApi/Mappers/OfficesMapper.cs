using AutoMapper;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Mappers;

public class OfficesMapper:Profile
{
    public OfficesMapper()
    {
        CreateMap<Office, GetOfficeResponse>();
        CreateMap<CreateOfficeRequest, Office>();
        CreateMap<EditOfficeRequest, Office>();
    }
    
    string InStringFormat(OfficeStatus status)
        => status switch
        {
            OfficeStatus.Active => "Active",
            OfficeStatus.Closed => "Closed",
            OfficeStatus.OnRepair => "Repair",
        };
    
}