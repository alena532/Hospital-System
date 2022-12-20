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
        
        await _patientRepository.CreateAsync(patient);
    }

    public async Task<Guid> CreateAccountAsync(CreatePatientAccountRequest request)
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
        await _accountRepository.CreateAsync(account);
        return account.Id;
    }

    public async Task<ICollection<GetPatientProfilesResponse>> GetMatchesAsync(CredentialsPatientProfileRequest request)
    {
        var patientsProfiles = await _patientRepository.GetMatchesAsync(request);
        ICollection<Patient> matchedPatients = new List<Patient>();
        int points = 0;
        foreach (var patient in patientsProfiles)
        {
            if (patient.FirstName.Equals(request.FirstName,StringComparison.OrdinalIgnoreCase))
            {
                points += (int) CredentialsPatientProfileEnum.FirstName;
            }
            
            if (patient.LastName.Equals(request.LastName,StringComparison.OrdinalIgnoreCase))
            {
                points += (int) CredentialsPatientProfileEnum.LastName;
            }
            
            if (patient.MiddleName.Equals(request.MiddleName,StringComparison.OrdinalIgnoreCase))
            {
                points += (int) CredentialsPatientProfileEnum.MiddleName;
            }
            
            if (patient.DateOfBirth.ToString().Equals(request.DateOfBirth.ToString(),StringComparison.OrdinalIgnoreCase))
            {
                points += (int) CredentialsPatientProfileEnum.DateOfBirth;
            }

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
    
    
}