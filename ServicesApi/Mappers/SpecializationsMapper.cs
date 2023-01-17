using AutoMapper;
using ServicesApi.Contracts.Requests.Specializations;
using ServicesApi.Contracts.Responses.Specializations;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.Mappers;

public class SpecializationsMapper:Profile
{
    public SpecializationsMapper()
    {
        CreateMap<CreateSpecializationRequest, Specialization>()
            .ForMember(x => x.Services, opt => opt.Ignore());
        CreateMap<Specialization, GetSpecializationByIdResponse>();
        CreateMap<EditSpecializationRequest, Specialization>()
            .ForMember(x => x.Services, opt => opt.Ignore());
        CreateMap<Specialization, GetSpecializationResponse>();
        

    }
}