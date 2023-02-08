using ProfilesApi.Contracts.Requests.PatientProfiles;
using ProfilesApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace ProfilesApi.DataAccess.Repositories.Interfaces.Base;

public interface IPatientProfileRepository:IRepositoryBase<Patient>
{
    Task CreateAsync(Patient patient);
    Task<IEnumerable<Patient>> GetMatchesAsync(CredentialsPatientProfileRequest parameters);
    Task<Patient> GetByIdAsync(Guid id, bool trackChanges=false);
    Task<Patient> GetByAccountIdAsync(Guid accountId, bool trackChanges=false);
    Task<IEnumerable<Patient>> GetAllAsync();
}