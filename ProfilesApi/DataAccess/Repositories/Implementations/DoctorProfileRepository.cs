using Microsoft.EntityFrameworkCore;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using RepositoryBase.Implementations;


namespace ProfilesApi.DataAccess.Repositories.Implementations;

public class DoctorProfileRepository: RepositoryBase<Doctor>,IDoctorProfileRepository
{
    
    public DoctorProfileRepository(AppDbContext repositoryContext): base(repositoryContext)
    {
    }
    
    public override async Task CreateAsync(Doctor doctor)
    {
        await base.CreateAsync(doctor);
    }

    public async Task<List<Doctor>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).ToListAsync();
    }
    
    public async Task<List<Doctor>> GetAllByOfficeIdAsync(Guid officeId,bool trackChanges)
    {
        return await FindByCondition(x=>x.OfficeId==officeId,trackChanges).ToListAsync();
    }

    public async Task<Doctor> GetByIdAsync(Guid id, bool trackChanges = false)
    {
        return await FindByCondition(x=>x.Id==id,trackChanges).SingleOrDefaultAsync();
    }
}