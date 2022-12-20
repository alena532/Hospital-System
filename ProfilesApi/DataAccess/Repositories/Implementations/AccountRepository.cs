using Microsoft.EntityFrameworkCore;
using ProfilesApi.DataAccess.Models;

using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using RepositoryBase.Implementations;

namespace ProfilesApi.DataAccess.Repositories.Implementations;

public class AccountRepository : RepositoryBase<Account>,IAccountRepository
{
    public AccountRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public override async Task CreateAsync(Account account)
    {
        await base.CreateAsync(account);
    }

    public async Task<Account> GetByIdAsync(Guid Id,bool trackChanges)
    {
        return await FindByCondition(x => x.Id == Id, trackChanges).SingleOrDefaultAsync();
    }

    
    
}