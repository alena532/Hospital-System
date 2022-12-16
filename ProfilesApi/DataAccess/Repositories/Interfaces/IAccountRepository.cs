using ProfilesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IAccountRepository:IRepositoryBase<Account>
{
    Task CreateAsync(Account account);

    Account GetById(Guid Id,bool trackChanges=false);
    //Task<Account> GetAccountForDoctorAsync ()
}