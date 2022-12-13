using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Base;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeReceptionistRepository:IRepositoryBase<OfficeReceptionist>
{
    Task<List<OfficeReceptionist>> GetOfficeReceptionistsAsync (Guid officeId, bool trackChanges);
    Task<OfficeReceptionist> GetOfficeReceptionistAsync(Guid officeId,Guid id, bool trackChanges);
    Task CreateOfficeReceptionistAsync(Guid officeId,OfficeReceptionist receptionist);
    Task DeleteReceptionistFromOfficeAsync(OfficeReceptionist receptionist);

}