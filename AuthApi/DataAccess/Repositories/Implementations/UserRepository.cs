using AuthApi.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryBase.Implementations;

namespace AuthApi.DataAccess.Repositories.Implementations;

public class UserRepository:RepositoryBase<User>, IUserRepository
{
    public UserRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    
    public async Task<User> GetByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}