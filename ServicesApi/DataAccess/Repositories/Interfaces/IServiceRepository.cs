using RepositoryBase.Interfaces;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.DataAccess.Repositories.Interfaces;

public interface IServiceRepository: IRepositoryBase<Service>
{
    Task CreateAsync(Service service);
    Task<Service> GetByIdAsync(Guid id, bool trackChanges = false);
    
}