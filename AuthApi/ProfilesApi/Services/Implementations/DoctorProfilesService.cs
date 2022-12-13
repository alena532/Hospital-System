using System.Security.Cryptography;
using AutoMapper;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Responses.DoctorProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Services.Implementations;

public class DoctorProfilesService:IDoctorProfilesService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IDoctorProfileRepository _doctorProfileRepository;

    public DoctorProfilesService(IMapper mapper,IAccountRepository accountRepository,IDoctorProfileRepository doctorProfileRepository)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _doctorProfileRepository = doctorProfileRepository;
    }
    
    public static string HashPassword(string password)
    {
        byte[] salt;
        byte[] buffer2;
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }
        byte[] dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }

    
    public async Task CreateAsync(CreateDoctorProfileRequest request)
    {
        var doctor = _mapper.Map<Doctor>(request);
        
        var account = _mapper.Map<Account>(request);
        
        account.CreatedAt = DateTime.Now;
        account.UpdateAt = DateTime.Now;
        account.IsEmailVerified = false;
        //when use jwt get from httpAccessor
        account.CreatedBy = request.OfficeId;
        account.UpdatedBy = request.OfficeId;
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string password = new string(Enumerable.Repeat(chars, 30)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());

        account.PasswordHash = HashPassword(password);

        await _accountRepository.CreateAccountForDoctorAsync(account);
        
        doctor.AccountId = account.Id;

        await _doctorProfileRepository.CreateDoctorProfileAsync(doctor);
        
    }

    public async Task<ICollection<GetDoctorProfilesResponse>> GetAllAsync(bool trackChanges)
    {
        var doctorsProfiles = await _doctorProfileRepository.GetAllDoctorProfilesAsync(trackChanges);
        return _mapper.Map<ICollection<GetDoctorProfilesResponse>>(doctorsProfiles);
    }
    
    public async Task<ICollection<GetDoctorProfilesResponse>> GetAllByOfficeIdAsync(Guid officeId,bool trackChanges)
    {
        var doctorsProfiles = await _doctorProfileRepository.GetAllDoctorProfilesByOfficeAsync(officeId,trackChanges);
        return _mapper.Map<ICollection<GetDoctorProfilesResponse>>(doctorsProfiles);
    }
    
    
}