using ProfilesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IPatientProfileRepository:IRepositoryBase<Patient>
{
    Task CreatePatientProfileAsync(Patient patient);
    Task<List<Patient>> GetPatientProfilesMatches(Patient patient);
    Task<Patient> GetPatientProfileAsync(Guid id, bool trackChanges);
}