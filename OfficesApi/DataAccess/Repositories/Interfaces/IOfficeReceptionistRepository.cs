using OfficesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeReceptionistRepository:IRepositoryBase<OfficeReceptionist>
{
    Task CreateOfficeReceptionistAsync(Guid officeId,OfficeReceptionist receptionist);
    Task<List<OfficeReceptionist>> GetOfficeReceptionistsAsync (Guid officeId, bool trackChanges=false);
    Task<OfficeReceptionist> GetOfficeReceptionistAsync(Guid officeId,Guid id, bool trackChanges=false);
    Task DeleteReceptionistFromOfficeAsync(OfficeReceptionist receptionist);

}