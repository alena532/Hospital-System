using AutoMapper;
using ServicesApi.Contracts.Requests.Specializations;
using ServicesApi.Contracts.Responses.Services;
using ServicesApi.Contracts.Responses.Specializations;
using ServicesApi.DataAccess.Models;
using ServicesApi.DataAccess.Repositories.Interfaces;
using ServicesApi.Services.Interfaces;

namespace ServicesApi.Services.Implementations;

public class SpecializationsService:ISpecializationsService
{
    private readonly IMapper _mapper;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IServiceRepository _serviceRepository;

    public SpecializationsService(IMapper mapper,ISpecializationRepository specializationRepository,IServiceRepository serviceRepository)
    {
        _mapper = mapper;
        _specializationRepository = specializationRepository;
        _serviceRepository = serviceRepository;
    }
    
    public async Task<GetSpecializationByIdResponse> CreateAsync(CreateSpecializationRequest request)
    {
        List<Service> services = new();
        foreach (var serviceId in request.Services)
        {
            services.Add(await _serviceRepository.GetByIdAsync(serviceId,true));
        }

        var specialization = _mapper.Map<Specialization>(request);
        specialization.Services = services;
        await _specializationRepository.CreateAsync(specialization);

        return _mapper.Map<GetSpecializationByIdResponse>(specialization);
    }

    public async Task<GetSpecializationByIdResponse> UpdateStatusAsync(EditSpecializationStatusRequest request)
    {
        var specialization = await _specializationRepository.GetByIdAsync(request.Id, true);
        if (specialization == null)
        {
            throw new BadHttpRequestException("Specialization nor found");
        }

        specialization.Status = request.Status;
        _specializationRepository.SaveChangesAsync();
        specialization.Services = await GetServicesWithServiceCategory(specialization.Services);

        return _mapper.Map<GetSpecializationByIdResponse>(specialization);
    }

    public async Task<GetSpecializationByIdResponse> UpdateAsync(EditSpecializationRequest request)
    {
        var specialization = await _specializationRepository.GetByIdAsync(request.Id);
        if (specialization == null)
        {
            throw new BadHttpRequestException("Specialization nor found");
        }
        
        List<Service> services = new();
        foreach (var serviceId in request.Services)
        {
            services.Add(await _serviceRepository.GetByIdAsync(serviceId,true));
        }

        _mapper.Map(request, specialization);
        specialization.Services = services;
        await _specializationRepository.UpdateAsync(specialization);
        
        return _mapper.Map<GetSpecializationByIdResponse>(specialization);
    }

    public async Task<GetSpecializationByIdResponse> GetByIdAsync(Guid id)
    {
        var specialization =  await _specializationRepository.GetByIdAsync(id);
        if (specialization == null)
        {
            throw new BadHttpRequestException("Specialization not found");
        }
        
        specialization.Services = await GetServicesWithServiceCategory(specialization.Services);

        return _mapper.Map<GetSpecializationByIdResponse>(specialization);
    }

    public async Task<IEnumerable<GetSpecializationResponse>> GetAllAsync()
    {
        var specializations = await _specializationRepository.GetAllAsync();
        return _mapper.Map<ICollection<GetSpecializationResponse>>(specializations);
    }

    public async Task<ICollection<Service>> GetServicesWithServiceCategory(ICollection<Service> servicesId)
    {
        List<Service> services = new();
        foreach (var service in servicesId)
        {
            services.Add(await _serviceRepository.GetByIdAsync(service.Id));
        }

        return services;
    }
}