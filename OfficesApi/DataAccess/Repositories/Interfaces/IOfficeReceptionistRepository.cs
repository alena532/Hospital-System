using OfficesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeReceptionistRepository:IRepositoryBase<OfficeReceptionist>
{
    Task<List<OfficeReceptionist>> GetOfficeReceptionistsAsync (Guid officeId, bool trackChanges=false);
    Task<OfficeReceptionist> GetOfficeReceptionistAsync(Guid officeId,Guid id, bool trackChanges=false);
    Task CreateOfficeReceptionistAsync(Guid officeId,OfficeReceptionist receptionist);
    Task DeleteReceptionistFromOfficeAsync(OfficeReceptionist receptionist);

}