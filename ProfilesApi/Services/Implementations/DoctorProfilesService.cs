using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Mail;
using ProfilesApi.Contracts.Requests;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;
using SharedModels.Routes;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace ProfilesApi.Services.Implementations;

public class DoctorProfilesService:IDoctorProfilesService
{
    private IWebHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IDoctorProfileRepository _doctorRepository;
    private readonly HttpClient _httpClient;
    private readonly IMailService _mailService;

    public DoctorProfilesService(IMapper mapper,IAccountRepository accountRepository,IDoctorProfileRepository doctorRepository,IMailService mailService,HttpClient http,IWebHostEnvironment env)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _doctorRepository = doctorRepository;
        _httpClient = new HttpClient();
        _mailService = mailService;
        _hostEnvironment = env;
    }
    
    public async Task<GetMailAndIdStuffResponse> CreateAsync(CreateDoctorProfileRequest request,Guid userId)
    {
        var checkEmail = _httpClient.PostAsJsonAsync( ApiRoutes.Auth + "api/AuthValidator",request.Email).Result;
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
            RoleId = new Guid("600d67af-f94a-4f14-f321-08db125c549f"),
            Password = password
        };

        var createdUser = await _httpClient.PostAsJsonAsync(ApiRoutes.Auth + "api/Auth/Register", authEntity);
        if (createdUser.IsSuccessStatusCode == false)
        {
            throw new BadHttpRequestException($"{createdUser.Content} {createdUser.ReasonPhrase}");
        }
        
        var userIdStream = await createdUser.Content.ReadAsStringAsync();
        var responseUserId = JsonSerializer.Deserialize<Guid>(userIdStream);
        
        var account = _mapper.Map<Account>(request);
        account.UserId = responseUserId;
        account.PhoneNumber = request.PhoneNumber;
        
        account.CreatedBy = userId;
        account.CreatedAt = DateTime.Now;

        try
        {
            await _accountRepository.CreateAsync(account);
        }
        catch
        {
            await RollBackUserAsync(account.UserId);
            throw;
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
            throw;
        }
        
        var mail = new MailForStuffConfirmationRequest()
        {
            ToEmail = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            AccountId = account.Id,
            Password = password
        };

        var mailResponse = await _mailService.SendEmailAsync(mail);
        var mailReturn = new GetMailAndIdStuffResponse()
        {
            mailResponse = mailResponse as GetMailForStuffResponse,
            Id = doctor.Id
        };
        return mailReturn;
    }
    
    public async Task ConfirmEmailAsync(Guid accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId,true);
        account.IsEmailVerified = true;
        await _accountRepository.SaveChangesAsync();
    }

    public async Task<GetDoctorProfilesResponse> UpdateAsync(EditDoctorProfileRequest request,Guid userId)
    {
        Doctor doctor =  await _doctorRepository.GetByIdAsync(request.Id);
        if (doctor == null)
        {
            throw new BadHttpRequestException("Doctor not found");
        }
        
        _mapper.Map(request, doctor);
        
        var account = await _accountRepository.GetByIdAsync(doctor.AccountId,true);
        if (account == null)
        {
            throw new BadHttpRequestException("Account not found");
        }
        account.UpdatedBy = userId;
        account.UpdateAt = DateTime.Now;

        await _accountRepository.SaveChangesAsync();
        
        await _doctorRepository.UpdateAsync(doctor);
        return _mapper.Map<GetDoctorProfilesResponse>(doctor);
    }

    public async Task<GetDoctorProfilesResponse> GetByIdAsync(Guid id)
    {
        var doctor =  await _doctorRepository.GetByIdAsync(id);
        if (doctor == null)
        {
            throw new BadHttpRequestException("Doctor not found");
        }
        
        return _mapper.Map<GetDoctorProfilesResponse>(doctor);
    }
    
    public async Task<GetDoctorProfilesResponse> GetByUserIdAsync(Guid userId)
    {
        var account = await _accountRepository.GetByUserIdAsync(userId);
        var doctor = await _doctorRepository.GetByAccountIdAsync(account.Id);
        if (doctor == null)
        {
            throw new BadHttpRequestException("Doctor not found");
        }
        
        return _mapper.Map<GetDoctorProfilesResponse>(doctor);
    }

    public async Task<bool> CheckEmailConfirmation(Guid userId)
    {
        var account = await _accountRepository.GetByUserIdAsync(userId);
        return account.IsEmailVerified;
    }
    
    public async Task<PageResult<GetDoctorAndPhotoProfilesResponse>> GetPageAsync(int pageNumber, int pageSize,SearchAndFilterParameters parameters)
    {
        var doctors = await _doctorRepository.SearchByCredentialsAsync(parameters.FirstName, parameters.LastName, parameters.OfficeId);
        var doctorsPerPage =
            _mapper.Map<List<GetDoctorAndPhotoProfilesResponse>>(doctors.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        foreach (var doctor in doctorsPerPage)
        {
            var photoResponse = await _httpClient.GetAsync(ApiRoutes.Documents + $"api/Photos/DoctorPhoto/{doctor.Id}");
            
            if (photoResponse.IsSuccessStatusCode)
            {
                var getPhotoResponse = JsonConvert.DeserializeObject<GetPhotoResponse>(await photoResponse.Content.ReadAsStringAsync());
                string uploads = Path.Combine(_hostEnvironment.ContentRootPath, "uploads/doctors");
                
                string filePath = Path.Combine(uploads,getPhotoResponse.FileName);
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    fs.Write(getPhotoResponse.Bytes);
                }
                
                doctor.Photo = getPhotoResponse.Bytes;
            }
        }
        
        var result = new PageResult<GetDoctorAndPhotoProfilesResponse>
        {
            Count = doctors.Count(),
            Items = doctorsPerPage
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