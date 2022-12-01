using OfficesApi.DataAccess.Models;

namespace OfficesApi.DataAccess.Repositories.Interfaces;

public interface IOfficeReceptionistRepository
{
    Task<List<OfficeReceptionist>> GetOfficeReceptionistsAsync (int officeId, bool trackChanges);
    Task<OfficeReceptionist> GetOfficeReceptionistAsync(int officeId,int id, bool trackChanges);
    Task CreateOfficeReceptionistAsync(int officeId,OfficeReceptionist receptionist);
    Task DeleteReceptionistFromOfficeAsync(OfficeReceptionist receptionist);

}