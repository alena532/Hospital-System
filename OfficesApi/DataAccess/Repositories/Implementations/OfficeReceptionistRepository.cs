using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Interfaces;
using RepositoryBase.Implementations;

namespace OfficesApi.DataAccess.Repositories.Implementations;

public class OfficeReceptionistRepository : RepositoryBase<OfficeReceptionist>, IOfficeReceptionistRepository
{
    public OfficeReceptionistRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public async Task CreateOfficeReceptionistAsync(Guid officeId,OfficeReceptionist receptionist)
    {
        receptionist.OfficeId = officeId;
        await CreateAsync(receptionist);
    }

    public async Task DeleteReceptionistFromOfficeAsync(OfficeReceptionist receptionist)
    {
        await DeleteAsync(receptionist);
    }
    
    public async Task<List<OfficeReceptionist>> GetOfficeReceptionistsAsync(Guid officeId,bool trackChanges)
    {
        return await FindByCondition(x => x.OfficeId.Equals(officeId),trackChanges:false).ToListAsync();
    }

    public async Task<OfficeReceptionist> GetOfficeReceptionistAsync(Guid officeId,Guid id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id) && p.OfficeId.Equals(officeId), trackChanges).SingleOrDefaultAsync();
    }
    
}
