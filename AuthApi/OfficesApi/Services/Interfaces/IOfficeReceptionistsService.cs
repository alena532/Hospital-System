using OfficesApi.Contracts.Requests.OfficeReceptionist;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.Contracts.Responses.OfficeReceptionist;
using OfficesApi.Contracts.Responses.Offices;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Services.Interfaces;

public interface IOfficeReceptionistsService
{
    public Task<ICollection<GetOfficeReceptionistResponse>> GetAllForOfficeAsync(int officeId);
    public Task DeleteFromOfficeAsync(OfficeReceptionist receptionist);
    public Task<GetOfficeReceptionistResponse> CreateForOfficeAsync(int officeId,CreateOfficeReceptionistRequest request);
    public  Task<GetOfficeReceptionistResponse> GetByIdForOfficeAsync(OfficeReceptionist receptionist);
    public Task<GetOfficeReceptionistResponse> UpdateForOfficeAsync(OfficeReceptionist receptionist,EditOfficeReceptionistRequest request);
    
}