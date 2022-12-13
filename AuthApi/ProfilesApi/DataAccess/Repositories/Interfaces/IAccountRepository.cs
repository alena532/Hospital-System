using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IAccountRepository
{
    Task CreateAccountForDoctorAsync(Account account);

    Account GetAccountById(Guid Id,bool trackChanges);
    //Task<Account> GetAccountForDoctorAsync ()
}