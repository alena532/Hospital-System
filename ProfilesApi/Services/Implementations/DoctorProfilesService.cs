using System.Net.Http.Headers;
using System.Security.Cryptography;
using AutoMapper;
using ProfilesApi.Contracts.Requests;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;


namespace ProfilesApi.Services.Implementations;

public class DoctorProfilesService:IDoctorProfilesService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IDoctorProfileRepository _repository;
    private readonly HttpClient _httpClient;

    public DoctorProfilesService(IMapper mapper,IAccountRepository accountRepository,IDoctorProfileRepository repository, HttpClient httpClient)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _repository = repository;
        _httpClient = new HttpClient();
    }
    
    
    public async Task CreateAsync(CreateDoctorProfileRequest request)
    {
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string password = new string(Enumerable.Repeat(chars, 30)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
        
        var authEntity = new RegisterRequest()
        {
            Email = request.Email,
            RoleId = request.RoleId,
            Password = password

        };
        var doctor = _mapper.Map<Doctor>(request);
        Guid UserId;
       //using (var client = new HttpClient())
       //{
           _httpClient.Timeout = TimeSpan.FromHours(1);
           try
           {
               var message = _httpClient.PostAsJsonAsync("https://localhost:5002/api/Auth/Register", authEntity).Result;

           }
           catch(Exception ex)
           {
               throw ex;
           }
       //}
       //var doctor = _mapper.Map<Doctor>(request);
       
       /*var account = _mapper.Map<Account>(request);
       
       account.CreatedAt = DateTime.Now;
       account.UpdateAt = DateTime.Now;
       account.IsEmailVerified = false;
       //when use jwt get from httpAccessor
       account.CreatedBy = request.OfficeId;
       account.UpdatedBy = request.OfficeId;
       
      
       //account.PasswordHash = HashPassword(password);

       await _accountRepository.CreateAsync(account);
       
       doctor.AccountId = account.Id;

       await _doctorProfileRepository.CreateAsync(doctor);
       */
    }

    public async Task<ICollection<GetDoctorProfilesResponse>> GetAllAsync()
    {
        var doctorsProfiles = await _repository.GetAllAsync();
        return _mapper.Map<ICollection<GetDoctorProfilesResponse>>(doctorsProfiles);
    }
    
    
    
    
}