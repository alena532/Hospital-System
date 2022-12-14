using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Implementations.Base;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;

namespace ProfilesApi.DataAccess.Repositories.Implementations;

public class AccountRepository : RepositoryBase<Account>,IAccountRepository
{
    public AccountRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public async Task CreateAccountForDoctorAsync(Account account)
    {
        await CreateAsync(account);
    }

    public Account GetAccountById(Guid Id,bool trackChanges)
    {
        return FindByCondition(x => x.Id == Id, trackChanges).SingleOrDefault();
    }

    
    
}