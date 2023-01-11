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

    //TODO: Use abstractions, not concrete implementations
    public async Task<IEnumerable<Doctor>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).ToListAsync();
    }
    
    public async Task<IEnumerable<Doctor>> GetAllByOfficeIdAsync(Guid officeId,bool trackChanges)
    {
        return await FindByCondition(x=>x.OfficeId==officeId,trackChanges).ToListAsync();
    }

    public async Task<Doctor> GetByIdAsync(Guid id, bool trackChanges = false)
    {
        return await FindByCondition(x=>x.Id==id,trackChanges).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Doctor>> SearchByCredentialsAsync(string firstName, string? lastName)
    {
        
        return await FindByCondition(x=>x.FirstName.ToLower().Contains(firstName.ToLower()) 
            && x.LastName.ToLower().Contains(lastName.ToLower()),trackChanges:false).ToListAsync();
    }
    
    public async Task<IEnumerable<Doctor>> SearchByCredentialsAsync(Guid? officeId, string? firstName, string? lastName)
    {
        IEnumerable<Doctor> result;
        IQueryable<Doctor> doctors = _repositoryContext.Set<Doctor>();
        doctors = doctors.Where(status => status.Status == DoctorStatusEnum.AtWork);

        if (officeId != null)
            doctors = doctors.Where(x => x.OfficeId == officeId);
        
        if (firstName != "null")
        {
            lastName = lastName != "null" ? lastName : "";
            doctors = doctors.Where(x =>
                x.FirstName.ToLower().Contains(firstName.ToLower()) &&
                x.LastName.ToLower().Contains(lastName.ToLower()));
        }
        result = await doctors.ToListAsync();
        return result;
    }
}