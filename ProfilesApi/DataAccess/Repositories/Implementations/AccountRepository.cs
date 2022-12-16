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

    public Account GetById(Guid Id,bool trackChanges)
    {
        return FindByCondition(x => x.Id == Id, trackChanges).SingleOrDefault();
    }

    
    
}