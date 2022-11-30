using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Interfaces;

namespace OfficesApi.DataAccess.Repositories.Implementations;

public class OfficeReceptionistRepository : RepositoryBase<OfficeReceptionist>, IOfficeReceptionistRepository
{
    public OfficeReceptionistRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public async Task CreateOfficeReceptionistAsync(OfficeReceptionist receptionist)
    {
        await CreateAsync(receptionist);
    }

    public async Task DeleteReceptionistFromOfficeAsync(OfficeReceptionist receptionist)
    {
        await DeleteAsync(receptionist);
    }
    
    public async Task<List<OfficeReceptionist>> GetOfficeReceptionistsAsync(int officeId,bool trackChanges)
    {
        return await FindByCondition(x => x.OfficeId.Equals(officeId),trackChanges:false).ToListAsync();
    }

    public async Task<OfficeReceptionist> GetOfficeReceptionistAsync(int officeId,int id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id) && p.OfficeId.Equals(officeId), trackChanges).SingleOrDefaultAsync();
    }
    
}
