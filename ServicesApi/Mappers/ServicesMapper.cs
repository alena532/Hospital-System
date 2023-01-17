using AutoMapper;
using ServicesApi.Contracts.Requests.Services;
using ServicesApi.Contracts.Responses.Services;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.Mappers;

public class ServicesMapper:Profile
{
    public ServicesMapper()
    {
        CreateMap<CreateServiceRequest, Service>();
        CreateMap<EditServiceRequest, Service>();
        CreateMap<Service, GetServiceResponse>()
            .ForMember(dest => dest.ServiceCategoryName,
                opt => opt.MapFrom(src => src.ServiceCategory.CategoryName));


    }
    
   
}