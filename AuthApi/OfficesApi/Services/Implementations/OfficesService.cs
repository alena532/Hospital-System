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
    private readonly IHttpContextAccessor _httpContextAccessor;
    
   
    public OfficesService(IMapper mapper,IOfficeRepository repository,IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        
    }

    public async Task<ICollection<GetOfficeResponse>> GetAllAsync()
    {
        var offices = await _repository.GetAllOfficesAsync(trackChanges: false);

        return _mapper.Map<ICollection<GetOfficeResponse>>(offices);
    }
    
    public async Task<GetOfficeResponse> GetByIdAsync(int id)
    {
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;

        return  _mapper.Map<GetOfficeResponse>(office);
    }
    
    public async Task DeleteAsync(int id)
    {
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;
        
        _repository.DeleteOffice(office);
        await _repository.SaveChangesAsync();
        
    }
    
    public async Task<GetOfficeResponse> CreateAsync(CreateOfficeRequest request)
    {
        var office = _mapper.Map<Office>(request);
        
        await _repository.CreateOffice(office);
        //await _repository.SaveChangesAsync();

        return _mapper.Map<GetOfficeResponse>(office);

    }

    public async Task<GetOfficeResponse> UpdateAsync(int id,EditOfficeRequest request)
    {
        var office = _httpContextAccessor.HttpContext.Items["office"] as Office;

        _mapper.Map(request, office);
        return _mapper.Map<GetOfficeResponse>(office);
    }
    
    

}