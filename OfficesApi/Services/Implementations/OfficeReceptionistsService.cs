using AutoMapper;
using OfficesApi.Contracts.Requests.OfficeReceptionist;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.OfficeReceptionist;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Interfaces;
using OfficesApi.Services.Interfaces;

namespace OfficesApi.Services.Implementations;

public class OfficeReceptionistsService:IOfficeReceptionistsService
{
    private readonly IMapper _mapper;
    private readonly IOfficeReceptionistRepository _repository;


    public OfficeReceptionistsService(IMapper mapper,IOfficeReceptionistRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ICollection<GetOfficeReceptionistResponse>> GetAllForOfficeAsync(Guid officeId)
    {
        var receptionists = await _repository.GetOfficeReceptionistsAsync(officeId,trackChanges: false);

        return _mapper.Map<ICollection<GetOfficeReceptionistResponse>>(receptionists);
    }
    
    public async Task<GetOfficeReceptionistResponse> GetByIdForOfficeAsync(OfficeReceptionist receptionist)
        => _mapper.Map<GetOfficeReceptionistResponse>(receptionist);

    public async Task DeleteFromOfficeAsync(OfficeReceptionist receptionist)
        => await _repository.DeleteReceptionistFromOfficeAsync(receptionist);


    public async Task<GetOfficeReceptionistResponse> CreateForOfficeAsync(Guid officeId,CreateOfficeReceptionistRequest request)
    {
        var receptionist = _mapper.Map<OfficeReceptionist>(request);
        
        await _repository.CreateOfficeReceptionistAsync(officeId,receptionist);

        return _mapper.Map<GetOfficeReceptionistResponse>(receptionist);
    }

    public async Task<GetOfficeReceptionistResponse> UpdateForOfficeAsync(OfficeReceptionist receptionist,EditOfficeReceptionistRequest request)
    {
        _mapper.Map(request, receptionist);
        _repository.SaveChangesAsync();
        return _mapper.Map<GetOfficeReceptionistResponse>(receptionist);
    }



    
}