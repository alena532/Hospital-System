using AutoMapper;
using ProfilesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IDoctorProfileRepository:IRepositoryBase<Doctor>
{  
   Task  CreateAsync(Doctor doctor);
   Task<List<Doctor>> GetAllAsync (bool trackChanges=false);

   Task<List<Doctor>> GetAllByOfficeIdAsync(Guid officeId, bool trackChanges=false);
   /*Task<Office> GetDoctorProfileAsync(int id, bool trackChanges);
   Task UpdateDoctorProfileAsync(Office office);
   Task<Office> FilterDoctorProfileBySpecializationAsync(int id, bool trackChanges);
   Task DeleteDoctorProfileAsync(Office office);
   Task<Office> FilterDoctorProfileByOfficeAsync(int id, bool trackChanges);
   
   Task<Office> SearchDoctorProfileByNameAsync(int id, bool trackChanges);
   */
}