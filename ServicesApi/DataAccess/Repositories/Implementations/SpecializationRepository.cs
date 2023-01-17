using Microsoft.EntityFrameworkCore;
using ServicesApi.DataAccess.Models;
using ServicesApi.DataAccess.Repositories.Interfaces;
using RepositoryBase.Implementations;

namespace ServicesApi.DataAccess.Repositories.Implementations;

public class SpecializationRepository:RepositoryBase<Specialization>,ISpecializationRepository
{
    public SpecializationRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<Specialization> GetByIdAsync(Guid id, bool trackChanges,bool withServices)
    {
        return await FindByCondition(x => x.Id == id, trackChanges).Include(x=>x.Services).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Specialization>> GetAllAsync(bool trackChanges = false)
    {
        return await FindAll(trackChanges).ToListAsync();
    }
    
}