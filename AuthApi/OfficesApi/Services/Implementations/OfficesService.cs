using AutoMapper;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Interfaces;
using OfficesApi.Services.Interfaces;


namespace OfficesApi.Services.Implementations;

public class OfficesService:IOfficesService
{
    private readonly IMapper _mapper;
    private readonly IOfficeRepository _repository;


    public OfficesService(IMapper mapper,IOfficeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ICollection<GetOfficeResponse>> GetAllAsync()
    {
        var offices = await _repository.GetAllOfficesAsync(trackChanges: false);

        return _mapper.Map<ICollection<GetOfficeResponse>>(offices);
    }
    
    public async Task<GetOfficeResponse> GetByIdAsync(Office office)
        => _mapper.Map<GetOfficeResponse>(office);
        
    
    public async Task DeleteAsync(Office office)
        => await _repository.DeleteOfficeAsync(office);


    public async Task<GetOfficeResponse> CreateAsync(CreateOfficeRequest request)
    {
        var office = _mapper.Map<Office>(request);
        
        await _repository.CreateOfficeAsync(office);

        return _mapper.Map<GetOfficeResponse>(office);
    }

    public async Task<GetOfficeResponse> UpdateAsync(Office office,EditOfficeRequest request)
    {
        _mapper.Map(request, office);
        return _mapper.Map<GetOfficeResponse>(office);
    }
    
}