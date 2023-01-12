using ProfilesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IAccountRepository:IRepositoryBase<Account>
{ 
    Task CreateAsync(Account account);
    Task<Account> GetByIdAsync(Guid id, bool trackChanges=false);
    Task<Account> GetByUserIdAsync(Guid userId, bool trackChanges=false);
}