using AutoMapper;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.Contracts.Responses.Accounts;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Mappers;

public class AccountsMapper:Profile
{
    public AccountsMapper()
    {
        CreateMap<CreateDoctorProfileRequest, Account>();
        CreateMap<CreatePatientAccountRequest, Account>();
        CreateMap<Account, GetAccountResponse>();
    }
    
   
}