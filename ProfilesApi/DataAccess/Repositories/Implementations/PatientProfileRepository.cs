using Microsoft.EntityFrameworkCore;
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
    
    public async Task CreatePatientProfileAsync(Patient patient)
    {
        await CreateAsync(patient);
    }

    public async Task<List<Patient>> GetPatientProfilesMatches(Patient patient)
    {
        return await FindByCondition(x =>
            x.IsLinkedToAccount==false &&
            x.FirstName.Equals(patient.FirstName) && x.LastName.Equals(patient.LastName) &&
            x.MiddleName.Equals(patient.MiddleName) && x.DateOfBirth.Equals(patient.DateOfBirth),trackChanges:false).ToListAsync();
    }

    public async Task<Patient> GetPatientProfileAsync(Guid id,bool trackChanges)
    {
        return await FindByCondition(x => x.Id == id,trackChanges).SingleOrDefaultAsync();
    }
    
}