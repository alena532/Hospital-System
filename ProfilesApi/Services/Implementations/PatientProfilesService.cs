using System.Text.Json;
using AutoMapper;
using Castle.Components.DictionaryAdapter;
using ProfilesApi.Contracts.Requests;
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
    private readonly HttpClient _httpClient;
    private readonly IPatientProfileRepository _patientRepository;

    public PatientProfilesService(IMapper mapper,IAccountRepository accountRepository,IPatientProfileRepository patientRepository,HttpClient httpClient)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _patientRepository = patientRepository;
        _httpClient = httpClient;
    }
    
    public async Task CreateAsync(CreatePatientProfileRequest request)
    {
        var patient = _mapper.Map<Patient>(request);
        patient.AccountId = request.AccountId;
        patient.IsLinkedToAccount = true;
       
        var account = await _accountRepository.GetByIdAsync(request.AccountId, trackChanges: true);
        account.PhoneNumber = request.PhoneNumber;
        //when use jwt get from httpAccessor
        //account.CreatedBy = request.OfficeId;
        //account.UpdatedBy = request.OfficeId;
        try
        {
            await _patientRepository.CreateAsync(patient);
        }
        catch
        {
            await RollBackUserAsync(account.UserId);
            await _accountRepository.DeleteAsync(account);
        }
    }

    public async Task<GetAccountUserCredentialsResponse> CreateAccountAsync(CreatePatientAccountRequest request)
    {
        var checkEmail = _httpClient.PostAsJsonAsync("api/AuthValidator",request.Email).Result;
        if (checkEmail.IsSuccessStatusCode == false)
        { 
            throw new BadHttpRequestException($"{checkEmail.Content} {checkEmail.ReasonPhrase}");
        }
        
        var authEntity = new RegisterRequest()
        {
            Email = request.Email,
            RoleId = request.RoleId,
            Password = request.Password
        };

        var createdUser = await _httpClient.PostAsJsonAsync("api/Auth/Register", authEntity);
        if (createdUser.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdUser.Content} {createdUser.ReasonPhrase}");
        }
        var userIdStream = await createdUser.Content.ReadAsStreamAsync();
        var userId = JsonSerializer.Deserialize<Guid>(userIdStream);
        
        var account = _mapper.Map<Account>(request);
        account.UserId = userId;
        //add createdBy UpdatedBy
        try
        {
            await _accountRepository.CreateAsync(account);
        }
        catch
        {
            await RollBackUserAsync(userId);
        }

        GetAccountUserCredentialsResponse response = new()
        {
            AccountId = account.Id,
            ToEmail = authEntity.Email
        };
        return response;
    }

    public async Task<ICollection<GetPatientProfilesResponse>> GetMatchesAsync(CredentialsPatientProfileRequest request)
    {
        var patientsProfiles = await _patientRepository.GetMatchesAsync(request);
        ICollection<Patient> matchedPatients = new List<Patient>();
        int points = 0;
        foreach (var patient in patientsProfiles)
        {
            points += patient.FirstName.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase)
                ? (int) CredentialsPatientProfileEnum.FirstName
                : 0;
            points += patient.LastName.Equals(request.LastName,StringComparison.OrdinalIgnoreCase)
                ? (int) CredentialsPatientProfileEnum.LastName
                : 0;
            points += patient.FirstName.Equals(request.MiddleName, StringComparison.OrdinalIgnoreCase)
                ? (int) CredentialsPatientProfileEnum.MiddleName
                : 0;
            points += patient.DateOfBirth.ToString().Equals(request.DateOfBirth.ToString(),StringComparison.OrdinalIgnoreCase)
                ? (int) CredentialsPatientProfileEnum.DateOfBirth
                : 0;

            if (points >= 13)
            {
                matchedPatients.Add(patient);
            }
        }
        return _mapper.Map<ICollection<GetPatientProfilesResponse>>(matchedPatients);
    }

    public async Task<GetPatientProfilesResponse> LinkPatientProfileToAccountAsync(Guid id)
    {
       var patient = await _patientRepository.GetByIdAsync(id, true);
       patient.IsLinkedToAccount = true;
       await _patientRepository.SaveChangesAsync();
       return _mapper.Map<GetPatientProfilesResponse>(patient);
    }

    public async Task RollBackUserAsync(Guid userId)
    {
        var result = _httpClient.DeleteAsync($"api/User/{userId}").Result;
        if (result.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{result.Content} {result.ReasonPhrase}");
        }
    }

    public async Task<Patient> GetByAccountId(Guid accountId)
    {
        return await _patientRepository.GetByAccountIdAsync(accountId);
    }
    
    
}