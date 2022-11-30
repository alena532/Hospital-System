using AutoMapper;
using OfficesApi.Contracts.Requests.OfficeReceptionist;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.OfficeReceptionist;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Mappers;

public class OfficeReceptionistsMapper:Profile
{
    public OfficeReceptionistsMapper()
    {
        CreateMap<OfficeReceptionist, GetOfficeReceptionistResponse>();
        CreateMap<CreateOfficeReceptionistRequest, Office>();
        CreateMap<EditOfficeReceptionistRequest, OfficeReceptionist>();
    }
}