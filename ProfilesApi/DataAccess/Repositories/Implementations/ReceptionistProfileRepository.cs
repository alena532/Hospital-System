using Microsoft.EntityFrameworkCore;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using RepositoryBase.Implementations;

namespace ProfilesApi.DataAccess.Repositories.Implementations;

public class ReceptionistProfileRepository: RepositoryBase<Receptionist>,IReceptionistProfileRepository
{
    public ReceptionistProfileRepository(AppDbContext repositoryContext): base(repositoryContext)
    {
    }
    
    public override async Task CreateAsync(Receptionist receptionist)
    {
        await base.CreateAsync(receptionist);
    }

    public async Task<Receptionist> GetByIdAsync(Guid id,bool trackChanges)
    {
        return await FindByCondition(x => x.Id == id, trackChanges).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Receptionist>> GetAllAsync()
    {
        return await FindAll(trackChanges: false).ToListAsync();
    }

    public async Task<IEnumerable<Receptionist>> GetAllByOfficeIdAsync(Guid officeId, bool trackChanges)
    {
        return await FindByCondition(x => x.OfficeId == officeId, trackChanges).ToListAsync();
    }
        

   
    
}