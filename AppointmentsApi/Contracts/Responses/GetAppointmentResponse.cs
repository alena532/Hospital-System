using AppointmentsApi.DataAccess.Models;

namespace AppointmentsApi.Contracts.Responses;

public class GetAppointmentResponse
{
    public Guid Id { get; set; }

    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    public TimeSpan Begin { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan End { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string PatientMiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public AppointmentStatus Status { get; set; }

}