using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Interfaces;

namespace OfficesApi.DataAccess.Repositories.Implementations;

public class OfficeRepository : RepositoryBase<Office>, IOfficeRepository
{
    public OfficeRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public async Task CreateOffice(Office office)
    {
        Create(office);
        await SaveChangesAsync();
    }

    public void DeleteOffice(Office office)
    {
        Delete(office);
    }
    
    public async Task<List<Office>> GetAllOfficesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).ToListAsync();
    }

    public async Task<Office> GetOfficeAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await SaveChangesAsync();
    }

}