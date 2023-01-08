using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Interfaces;
using RepositoryBase.Implementations;

namespace OfficesApi.DataAccess.Repositories.Implementations;

public class OfficeRepository : RepositoryBase<Office>, IOfficeRepository
{
    public OfficeRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public async override Task CreateAsync(Office office)
    {
        await base.CreateAsync(office);
    }
    
    public async Task<List<Office>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).ToListAsync();
    }

    public async Task<Office> GetByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }

    public async override Task DeleteAsync(Office office)
    {
        await base.DeleteAsync(office);
    }

}