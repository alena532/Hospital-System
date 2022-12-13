using AutoMapper;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IDoctorProfileRepository:IRepositoryBase<Doctor>
{
    Task CreateDoctorProfileAsync(Doctor doctor);
   Task<List<Doctor>> GetAllDoctorProfilesAsync (bool trackChanges);

   Task<List<Doctor>> GetAllDoctorProfilesByOfficeAsync(Guid officeId, bool trackChanges);
   /*Task<Office> GetDoctorProfileAsync(int id, bool trackChanges);
   Task UpdateDoctorProfileAsync(Office office);
   Task<Office> FilterDoctorProfileBySpecializationAsync(int id, bool trackChanges);
   Task DeleteDoctorProfileAsync(Office office);
   Task<Office> FilterDoctorProfileByOfficeAsync(int id, bool trackChanges);
   
   Task<Office> SearchDoctorProfileByNameAsync(int id, bool trackChanges);
   */
}