using OfficesApi.DataAccess.Models;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeRepository
{
    Task<List<Office>> GetAllOfficesAsync (bool trackChanges);
    Task<Office> GetOfficeAsync(Guid id, bool trackChanges);
    Task CreateOffice(Office office);
    void DeleteOffice(Office office);
    Task SaveChangesAsync();
}