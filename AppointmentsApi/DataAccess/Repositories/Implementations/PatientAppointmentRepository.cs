using AppointmentsApi.DataAccess.Models;
using AppointmentsApi.DataAccess.Repositories.Interfaces;
using RepositoryBase.Implementations;

namespace AppointmentsApi.DataAccess.Repositories.Implementations;

public class PatientAppointmentRepository:RepositoryBase<PatientAppointment>,IPatientAppointmentRepository
{
    public PatientAppointmentRepository(AppDbContext repositoryContext)
        : base(repositoryContext)
    {
    }
}