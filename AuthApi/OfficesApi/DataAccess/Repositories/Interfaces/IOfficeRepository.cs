using OfficesApi.DataAccess.Models;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeRepository
{
    Task<List<Office>> GetAllOfficesAsync (bool trackChanges);
    Task<Office> GetOfficeAsync(int id, bool trackChanges);
    Task CreateOfficeAsync(Office office);
    Task DeleteOfficeAsync(Office office);
}