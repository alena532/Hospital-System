using AppointmentsApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace AppointmentsApi.DataAccess.Repositories.Interfaces;

public interface IAppointmentRepository:IRepositoryBase<Appointment>
{
   Task<IEnumerable<Appointment>> GetAllAsync(bool trackChanges = false);
  Task<Appointment> GetByIdAsync(Guid id, bool trackChanges = false);

}