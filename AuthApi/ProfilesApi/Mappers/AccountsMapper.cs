using AutoMapper;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Mappers;

public class AccountsMapper:Profile
{
    public AccountsMapper()
    {
        CreateMap<CreateDoctorProfileRequest, Account>();

    }
    
   
}