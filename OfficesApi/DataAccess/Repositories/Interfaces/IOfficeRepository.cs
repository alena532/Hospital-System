using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Base;
using OfficesApi.DataAccess.Repositories.Implementations;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeRepository:IRepositoryBase<Office>
{
    Task<List<Office>> GetAllOfficesAsync (bool trackChanges);
    Task<Office> GetOfficeAsync(Guid id, bool trackChanges);
    Task CreateOfficeAsync(Office office);
    Task DeleteOfficeAsync(Office office);
    
}