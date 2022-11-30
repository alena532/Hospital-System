using AutoMapper;
using OfficesApi.Contracts.Responses.OfficeReceptionist;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Mappers;

public class OfficeReceptionistsMapper:Profile
{
    public OfficeReceptionistsMapper()
    {
        CreateMap<OfficeReceptionist, GetOfficeReceptionistResponse>();
        
    }
}