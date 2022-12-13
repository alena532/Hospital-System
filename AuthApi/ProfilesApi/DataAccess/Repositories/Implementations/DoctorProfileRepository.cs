using Microsoft.EntityFrameworkCore;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Implementations.Base;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;

namespace ProfilesApi.DataAccess.Repositories.Implementations;

public class DoctorProfileRepository:RepositoryBase<Doctor>,IDoctorProfileRepository
{
    
    public DoctorProfileRepository(AppDbContext repositoryContext): base(repositoryContext)
    {
    }
    
    public async Task CreateDoctorProfileAsync(Doctor doctor)
    {
        await CreateAsync(doctor);
    }

    public async Task<List<Doctor>> GetAllDoctorProfilesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).ToListAsync();
    }
    
    public async Task<List<Doctor>> GetAllDoctorProfilesByOfficeAsync(Guid officeId,bool trackChanges)
    {
        return await FindByCondition(x=>x.OfficeId==officeId,trackChanges).ToListAsync();
    }
    
}