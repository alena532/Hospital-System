using Microsoft.EntityFrameworkCore;
using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;
using RepositoryBase.Implementations;

namespace ProfilesApi.DataAccess.Repositories.Implementations;

public class PatientProfileRepository: RepositoryBase<Patient>,IPatientProfileRepository
{
    public PatientProfileRepository(AppDbContext repositoryContext): base(repositoryContext)
    {
    }
    
    public override async Task CreateAsync(Patient patient)
    {
        await base.CreateAsync(patient);
    }

    public async Task<List<Patient>> GetMatchesAsync(CredentialsPatientProfileRequest parameters)
    {
        return await FindByCondition(x =>
            x.IsLinkedToAccount==false &&
            (x.FirstName.Equals(parameters.FirstName,StringComparison.OrdinalIgnoreCase) || x.LastName.Equals(parameters.LastName,StringComparison.OrdinalIgnoreCase) ||
            x.MiddleName.Equals(parameters.MiddleName,StringComparison.OrdinalIgnoreCase) || x.DateOfBirth.ToString().Equals(parameters.DateOfBirth.ToString(),StringComparison.OrdinalIgnoreCase)),trackChanges:true).ToListAsync();
    }

    public async Task<Patient> GetByIdAsync(Guid id,bool trackChanges)
    {
        return await FindByCondition(x => x.Id == id,trackChanges).SingleOrDefaultAsync();
    }

    public async Task<Patient> GetByAccountIdAsync(Guid accountId, bool trackChanges)
    {
        return await FindByCondition(x => x.AccountId == accountId,trackChanges).SingleOrDefaultAsync();
    }
}