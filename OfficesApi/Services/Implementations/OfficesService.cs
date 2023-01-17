using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        var offices = await _repository.GetAllAsync();

        return _mapper.Map<ICollection<GetOfficeResponse>>(offices);
    }

    public async Task<GetOfficeResponse> GetByIdAsync(Guid id)
    {
       var office =  await _repository.GetByIdAsync(id);
       if (office == null)
       {
           throw new BadHttpRequestException("Office doesnt found");
       }

       return _mapper.Map<GetOfficeResponse>(office);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var office =  await _repository.GetByIdAsync(id);
        if (office == null)
        {
            throw new BadHttpRequestException("Office doesnt found");
        }
        await _repository.DeleteAsync(office);
    }

    public async Task<GetOfficeResponse> CreateAsync(CreateOfficeRequest request)
    {
        var office = _mapper.Map<Office>(request);
        await _repository.CreateAsync(office);
        return _mapper.Map<GetOfficeResponse>(office);
    }

    public async Task<GetOfficeResponse> UpdateAsync(EditOfficeRequest request)
    {
        var office =  await _repository.GetByIdAsync(request.Id);
        if (office == null)
        {
            throw new BadHttpRequestException("Office doesnt found");
        }
        
        _mapper.Map(request, office);
        await _repository.UpdateAsync(office);
        return _mapper.Map<GetOfficeResponse>(office);
    }
    
    public async Task<GetOfficeResponse> UpdateStatusAsync(EditOfficeStatusRequest request)
    {
        var office =  await _repository.GetByIdAsync(request.Id,true);
        if (office == null)
        {
            throw new BadHttpRequestException("Office doesnt found");
        }
        
        office.Status = request.Status;
        await _repository.SaveChangesAsync();
        return _mapper.Map<GetOfficeResponse>(office);
    }
    
}