using Microsoft.EntityFrameworkCore;
using ServicesApi.DataAccess.Models;
using ServicesApi.DataAccess.Repositories.Interfaces;
using RepositoryBase.Implementations;


namespace ServicesApi.DataAccess.Repositories.Implementations;

public class ServiceRepository : RepositoryBase<Service>,IServiceRepository
{
    public ServiceRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public override async Task CreateAsync(Service service)
    {
        await base.CreateAsync(service);
    }
    
    public async Task<Service> GetByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id), trackChanges).Include(x => x.ServiceCategory)
            .SingleOrDefaultAsync();
    }
    
    
}