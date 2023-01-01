using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Implementations;
using RepositoryBase.Interfaces;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeRepository:IRepositoryBase<Office>
{
    Task<List<Office>> GetAllAsync (bool trackChanges=false);
    Task<Office> GetByIdAsync(Guid id, bool trackChanges=false );
    Task CreateAsync(Office office);
    //Task DeleteAsync(Office office);
    
}