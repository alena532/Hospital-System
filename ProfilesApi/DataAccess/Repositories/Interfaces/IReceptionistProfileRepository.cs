using ProfilesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IReceptionistProfileRepository :IRepositoryBase<Receptionist>
{
    Task CreateAsync(Receptionist receptionist);
    Task<Receptionist> GetByIdAsync(Guid id, bool trackChanges=false);
    Task<IEnumerable<Receptionist>> GetAllAsync();
    Task<IEnumerable<Receptionist>> GetAllByOfficeIdAsync(Guid officeId, bool trackChanges=false);

}