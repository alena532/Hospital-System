using AutoMapper;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.PatientProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Services.Implementations;

public class PatientProfilesService : IPatientProfilesService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IPatientProfileRepository _repository;

    public PatientProfilesService(IMapper mapper,IAccountRepository accountRepository,IPatientProfileRepository repository)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _repository = repository;
    }
    
    public async Task CreateAsync(CreatePatientProfileRequest request)
    {
        var patient = _mapper.Map<Patient>(request);
        patient.AccountId = request.AccountId;
        var account = _accountRepository.GetAccountById(request.AccountId, trackChanges: true);

        account.CreatedAt = DateTime.Now;
        account.UpdateAt = DateTime.Now;

        account.PhoneNumber = request.PhoneNumber;
        //when use jwt get from httpAccessor
        //account.CreatedBy = request.OfficeId;
        //account.UpdatedBy = request.OfficeId;
        await _repository.CreatePatientProfileAsync(patient);
    }

    public async Task<List<GetPatientProfilesResponse>> GetMatchesAsync(Guid id)
    {
        List<GetPatientProfilesResponse> patientsProfiles = new();
        var patient = await _repository.GetPatientProfileAsync(id, trackChanges: false);
        if (patient != null)
        {
            var patients = _repository.GetPatientProfilesMatches(patient);
            patientsProfiles = _mapper.Map<List<GetPatientProfilesResponse>>(patients);
        }

        return patientsProfiles;

    }
}