using RepositoryBase.Interfaces;

namespace AuthApi.DataAccess.Repositories.Interfaces;

public interface IUserRepository:IRepositoryBase<User>
{
    Task<User> GetByIdAsync(Guid id,bool trackChanges=false);
    
}