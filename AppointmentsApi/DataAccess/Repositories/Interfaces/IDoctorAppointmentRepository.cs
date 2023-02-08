using AppointmentsApi.DataAccess.Models;
using RepositoryBase.Interfaces;

namespace AppointmentsApi.DataAccess.Repositories.Interfaces;

public interface IDoctorAppointmentRepository:IRepositoryBase<DoctorAppointment>
{
    public Task<IEnumerable<Appointment>> GetSortedTimeByDateAndDoctorIdAsync(Guid doctorId,
        DateTime appointmentDate, bool trackChanges = false);
    
    
}