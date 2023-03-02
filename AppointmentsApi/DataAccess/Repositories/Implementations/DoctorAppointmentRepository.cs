using AppointmentsApi.DataAccess.Models;
using AppointmentsApi.DataAccess.Repositories.Interfaces;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using RepositoryBase.Implementations;

namespace AppointmentsApi.DataAccess.Repositories.Implementations;

public class DoctorAppointmentRepository:RepositoryBase<DoctorAppointment>,IDoctorAppointmentRepository
{
    public DoctorAppointmentRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Appointment>> GetSortedTimeByDateAndDoctorIdAsync(Guid doctorId,DateTime appointmentDate,bool trackChanges)
    {
        return await FindByCondition(x => x.DoctorId == doctorId,trackChanges)
            .Include(x=>x.Appointments)
            .SelectMany(x=>x.Appointments)
            .Where(x=>x.Date==appointmentDate)
            .OrderBy(x=>x.Begin)
            .ToListAsync();
    }

    
}