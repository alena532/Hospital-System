using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProfilesApi.Contracts;
using ProfilesApi.Contracts.Mail;
using ProfilesApi.Contracts.ReceptionistProfiles;
using ProfilesApi.Contracts.Requests;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.Contracts.Requests.ReceptionistProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;
using SharedModels.Routes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProfilesApi.Services.Implementations;

public class ReceptionistProfilesService:IReceptionistProfilesService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly HttpClient _httpClient;
    private readonly IReceptionistProfileRepository _receptionistRepository;
    private readonly IDoctorProfilesService _doctorService;
    private readonly IMailService _mailService;
    private IWebHostEnvironment _hostEnvironment;
    private readonly IMemoryCache _memoryCache;

    public ReceptionistProfilesService(IMapper mapper,IAccountRepository accountRepository,IReceptionistProfileRepository receptionistRepository,HttpClient httpClient,IDoctorProfilesService doctorService,IMailService mailService,IWebHostEnvironment hostEnvironment,IMemoryCache memoryCache)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _receptionistRepository = receptionistRepository;
        _httpClient = httpClient;
        _doctorService = doctorService;
        _mailService = mailService;
        _hostEnvironment = hostEnvironment;
        _memoryCache = memoryCache;
    }
    
    public async Task<GetMailAndIdStuffResponse> CreateAsync(CreateReceptionistProfileRequest request)
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
            RoleId = new Guid("cbfe46ec-b39d-460e-f323-08db125c549f"),
            Password = password
        };

        var createdUser = await _httpClient.PostAsJsonAsync(ApiRoutes.Auth + "api/Auth/Register", authEntity);
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
            await _doctorService.RollBackUserAsync(account.UserId);
            throw;
        }
        
        var receptionist = _mapper.Map<Receptionist>(request);
        receptionist.AccountId = account.Id;
        try
        {
            await _receptionistRepository.CreateAsync(receptionist);
        }
        catch
        {
            await _doctorService.RollBackUserAsync(account.UserId);
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
        var response = new GetMailAndIdStuffResponse()
        {
            Id = receptionist.Id,
            mailResponse = mailResponse as GetMailForStuffResponse
        };
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        var receptionist = await _receptionistRepository.GetByIdAsync(id);
        if (receptionist == null)
            throw new BadHttpRequestException("Receptionist not found");

        await _receptionistRepository.DeleteAsync(receptionist);
    }
    
    public async Task<GetDetailedReceptionistProfilesResponse> GetByUserIdAsync(Guid userId)
    {
        var accountReceptionist = await _accountRepository.FindByCondition(x=>x.UserId==userId,trackChanges:false).Include(x=>x.Receptionist).SingleOrDefaultAsync();
        if (accountReceptionist == null || accountReceptionist.Receptionist == null)
            throw new BadHttpRequestException("Receptionist not found");

        var receptionist = await _receptionistRepository.GetByIdAsync(accountReceptionist.Receptionist.Id);
        if(receptionist == null)
            throw new BadHttpRequestException("Receptionist not found");
        
        return _mapper.Map<GetDetailedReceptionistProfilesResponse>(receptionist);
    }
    
    public async Task<GetDetailedReceptionistProfilesResponse> GetByIdAsync(Guid id)
    {
        var receptionist = await _receptionistRepository.GetByIdAsync(id);
        if(receptionist == null)
            throw new BadHttpRequestException("Receptionist not found");
        
        return _mapper.Map<GetDetailedReceptionistProfilesResponse>(receptionist);
    }

    public async Task<PageResult<GetReceptionistAndPhotoProfilesResponse>> GetPageAsync(int pageNumber, int pageSize)
    {
        var receptionists = await _receptionistRepository.GetAllAsync();
        var transformedReceptionists =_mapper.Map<List<GetReceptionistAndPhotoProfilesResponse>>(receptionists.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        foreach (var receptionist in transformedReceptionists)
        {
            var photoResponse = await _httpClient.GetAsync(ApiRoutes.Documents + $"api/Photos/ReceptionistPhoto/{receptionist.Id}");

            if (photoResponse.IsSuccessStatusCode)
            {
                var getPhotoResponse = JsonConvert.DeserializeObject<GetPhotoResponse>(await photoResponse.Content.ReadAsStringAsync());
                string uploads = Path.Combine(_hostEnvironment.ContentRootPath, "uploads/receptionists");
                
                string filePath = Path.Combine(uploads,getPhotoResponse.FileName);
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    fs.Write(getPhotoResponse.Bytes);
                }
                
                receptionist.Photo = getPhotoResponse.Bytes;
            }
        
        }
        
        var result = new PageResult<GetReceptionistAndPhotoProfilesResponse>
        {
            Count = receptionists.Count(),
            Items = transformedReceptionists
        };
        return result;
    }

    public async Task<PageResult<GetReceptionistAndPhotoProfilesResponse>> GetAllAsync()
    {
        var cacheKey = "receptionists";
        if(!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Receptionist> receptionists))
        {
            receptionists = await _receptionistRepository.GetAllAsync();
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(cacheKey, receptionists, cacheExpiryOptions);
        }
        var transformedReceptionists =_mapper.Map<List<GetReceptionistAndPhotoProfilesResponse>>(receptionists);
        foreach (var receptionist in transformedReceptionists)
        {
            var photoResponse = await _httpClient.GetAsync(ApiRoutes.Documents + $"api/Photos/ReceptionistPhoto/{receptionist.Id}");
            
            if (photoResponse.IsSuccessStatusCode)
            {
                var getPhotoResponse = JsonConvert.DeserializeObject<GetPhotoResponse>(await photoResponse.Content.ReadAsStringAsync());
                if (getPhotoResponse.FileName != null && getPhotoResponse.Bytes != null)
                {
                    string uploads = Path.Combine(_hostEnvironment.ContentRootPath, "uploads/receptionists");
                
                    string filePath = Path.Combine(uploads,getPhotoResponse.FileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        fs.Write(getPhotoResponse.Bytes);
                    }
                
                    receptionist.Photo = getPhotoResponse.Bytes;
                }
            }
        }
        
        var result = new PageResult<GetReceptionistAndPhotoProfilesResponse>
        {
            Count = receptionists.Count(),
            Items = transformedReceptionists
        };
        return result;
        
    }
    
    
        
}