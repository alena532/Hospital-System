using RepositoryBase.Interfaces;
using ServicesApi.DataAccess.Models;

namespace ServicesApi.DataAccess.Repositories.Interfaces;

public interface ISpecializationRepository:IRepositoryBase<Specialization>
{
    Task<Specialization> GetByIdAsync(Guid id, bool trackChanges = false,bool withServices=true);
    Task<IEnumerable<Specialization>> GetAllAsync(bool trackChanges = false);
}