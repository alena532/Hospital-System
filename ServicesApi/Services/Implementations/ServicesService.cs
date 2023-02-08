using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicesApi.Contracts.Requests.Services;
using ServicesApi.Contracts.Responses.Services;
using ServicesApi.DataAccess.Models;
using ServicesApi.DataAccess.Repositories.Interfaces;
using ServicesApi.Services.Interfaces;

namespace ServicesApi.Services.Implementations;

public class ServicesService:IServicesService
{
    private readonly IMapper _mapper;
    private readonly IServiceRepository _serviceRepository;

    public ServicesService(IMapper mapper,IServiceRepository serviceRepository)
    {
        _mapper = mapper;
        _serviceRepository = serviceRepository;
    }
    
    public async Task<GetServiceResponse> CreateAsync(CreateServiceRequest request)
    {
        var service = _mapper.Map<Service>(request);
        await _serviceRepository.CreateAsync(service);
        service = await _serviceRepository.GetByIdAsync(service.Id);
        return _mapper.Map<GetServiceResponse>(service);
    }

    public async Task<GetServiceResponse> UpdateStatusAsync(EditServiceStatusRequest request)
    {
        var service = await _serviceRepository.GetByIdAsync(request.Id,true);
        if (service == null)
        {
            throw new BadHttpRequestException("Service not found");
        }

        service.Status = request.Status;
        await _serviceRepository.SaveChangesAsync();
        return _mapper.Map<GetServiceResponse>(service);
    }

    public async Task<GetServiceResponse> UpdateAsync(EditServiceRequest request)
    {
        var service = await _serviceRepository.GetByIdAsync(request.Id,true);
        if (service == null)
        {
            throw new BadHttpRequestException("Service not found");
        }
        
        _mapper.Map(request, service);
        await _serviceRepository.UpdateAsync(service);
        return _mapper.Map<GetServiceResponse>(service);
    }

    public async Task<GetServiceResponse> GetByIdAsync(Guid id)
    {
        var service =  await _serviceRepository.GetByIdAsync(id);
        if (service == null)
        {
            throw new BadHttpRequestException("Service not found");
        }

        return _mapper.Map<GetServiceResponse>(service);
    }

    public async Task<IEnumerable<GetServiceResponse>> GetAllByMissingSpecializationAsync()
    {
        var services = await _serviceRepository.FindByCondition(x => x.SpecializationId == null,false).Include(x=>x.ServiceCategory).ToListAsync();
        
        return _mapper.Map<ICollection<GetServiceResponse>>(services);
    }
}