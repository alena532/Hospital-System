using AppointmentsApi.DataAccess.Models;
using AppointmentsApi.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryBase.Implementations;

namespace AppointmentsApi.DataAccess.Repositories.Implementations;

public class AppointmentRepository:RepositoryBase<Appointment>,IAppointmentRepository
{
    public AppointmentRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).Include(x => x.DoctorAppointment)
            .Include(x => x.PatientAppointment)
            .ToListAsync();
    }
    
    public async Task<Appointment> GetByIdAsync(Guid id,bool trackChanges)
    {
        return await FindByCondition(x => x.Id.Equals(id), trackChanges).Include(x => x.DoctorAppointment)
            .Include(x => x.PatientAppointment)
            .SingleOrDefaultAsync();
    }
    
}