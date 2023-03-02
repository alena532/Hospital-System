using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsApi.DataAccess.Models;

public class Appointment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    public TimeSpan Begin { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan End { get; set; }
    public AppointmentStatus Status { get; set; }
    public Guid DoctorAppointmentId { get; set; }
    public Guid PatientAppointmentId { get; set; }
    public virtual DoctorAppointment DoctorAppointment { get; set; }
    public virtual PatientAppointment PatientAppointment { get; set; }
}