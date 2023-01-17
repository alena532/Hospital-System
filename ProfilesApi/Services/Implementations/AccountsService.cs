using AutoMapper;
using ProfilesApi.Contracts.Responses.Accounts;
using ProfilesApi.Contracts.Responses.PatientProfiles;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Services.Implementations;

public class AccountsService : IAccountsService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IPatientProfileRepository _patientRepository;

    public AccountsService(IMapper mapper,IAccountRepository accountRepository, IPatientProfileRepository patientRepository)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _patientRepository = patientRepository;
    }

    public async Task CheckPatientAccountBeforeProfileCreationAsync(Guid id)
    {
        var account = await _accountRepository.GetByIdAsync(id,trackChanges:false);
        if (account == null)
        {
            throw new BadHttpRequestException("Account doesn't found");
        }

        var patient = await _patientRepository.GetByAccountIdAsync(account.Id);
        if (patient != null)
        {
            throw new BadHttpRequestException("Patient was found");
        }
    }
    
    public async Task<GetAccountAndPatientProfileResponse> CheckPatientAccountBeforeProfileLoginAsync(Guid userId)
    {
        var account = await _accountRepository.GetByUserIdAsync(userId,trackChanges:false);
        if (account == null)
        {
            throw new BadHttpRequestException("Account doesn't found");
        }

        var patient = await _patientRepository.GetByAccountIdAsync(account.Id);
        if (patient == null)
        {
            throw new BadHttpRequestException("Patient was found");
        }

        var response = new GetAccountAndPatientProfileResponse()
        {
            Patient = _mapper.Map<GetPatientProfilesResponse>(patient),
            Email = account.Email,
            PhoneNumber = account.PhoneNumber,
        };
        return response;
    }
}