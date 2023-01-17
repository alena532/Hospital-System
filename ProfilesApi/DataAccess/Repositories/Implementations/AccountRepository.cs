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

    public async Task<Account> GetByIdAsync(Guid id,bool trackChanges)
    {
        var accounts = FindByCondition(x => x.Id == id, trackChanges);
        return accounts.SingleOrDefault();
    }

    public async Task<Account> GetByUserIdAsync(Guid userId, bool trackChanges)
    {
        return await FindByCondition(x => x.UserId == userId, trackChanges).SingleOrDefaultAsync();
    }
    
}