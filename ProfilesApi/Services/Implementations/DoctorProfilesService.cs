using System.Text.Json;
using AutoMapper;
using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Requests;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;


namespace ProfilesApi.Services.Implementations;

public class DoctorProfilesService:IDoctorProfilesService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IDoctorProfileRepository _doctorRepository;
    private readonly HttpClient _httpClient;
    private readonly IMailService _mailService;

    public DoctorProfilesService(IMapper mapper,IAccountRepository accountRepository,IDoctorProfileRepository doctorRepository,HttpClient httpClient,IMailService mailService)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _doctorRepository = doctorRepository;
        _httpClient = httpClient;
        _mailService = mailService;
    }
    
    public async Task CreateAsync(CreateDoctorProfileRequest request)
    {
        var checkEmail = _httpClient.PostAsJsonAsync("api/AuthValidator",request.Email).Result;
        if (checkEmail.IsSuccessStatusCode == false)
        { 
            throw new BadHttpRequestException($"{checkEmail.Content} {checkEmail.ReasonPhrase}");
        }
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string password = new string(Enumerable.Repeat(chars, 30)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
        
        var authEntity = new RegisterRequest()
        {
            Email = request.Email,
            RoleId = request.RoleId,
            Password = password
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
        account.PhoneNumber = request.PhoneNumber;
        //add createdBy UpdatedBy
        try
        {
            await _accountRepository.CreateAsync(account);
        }
        catch
        {
            await RollBackUserAsync(account.UserId);
        }
        
        var doctor = _mapper.Map<Doctor>(request);
        doctor.AccountId = account.Id;
        try
        {
            await _doctorRepository.CreateAsync(doctor);
        }
        catch
        {
            await RollBackUserAsync(account.UserId);
            await _accountRepository.DeleteAsync(account);
        }
        
        var mail = new MailForDoctorConfirmationRequest()
        {
            ToEmail = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            AccountId = account.Id
        };

        await _mailService.SendEmailAsync(mail);
    }
    
    public async Task ConfirmEmailAsync(Guid id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id, trackChanges: true);
        var account = await _accountRepository.GetByIdAsync(doctor.AccountId, trackChanges: true);
        account.IsEmailVerified = true;
    }
    
    public async Task<PageResult<GetDoctorProfilesResponse>> GetAllAsync(int pageNumber, int pageSize,SearchAndFilterParameters parameters)
    {
        var doctors = await _doctorRepository.SearchByCredentialsAsync(parameters.FirstName, parameters.LastName, parameters.OfficeId);
        
        var result = new PageResult<GetDoctorProfilesResponse>
        {
            Count = doctors.Count(),
            Items = _mapper.Map<List<GetDoctorProfilesResponse>>(doctors.Skip((pageNumber - 1) * pageSize).Take(pageSize))
        };
        return result;
    }
    
    public async Task RollBackUserAsync(Guid userId)
    {
        var result = _httpClient.DeleteAsync($"api/User/{userId}").Result;
        if (result.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{result.Content} {result.ReasonPhrase}");
        }
    }
    
}