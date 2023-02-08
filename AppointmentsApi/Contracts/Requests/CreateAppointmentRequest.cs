using System.ComponentModel.DataAnnotations;
using AppointmentsApi.DataAccess.Models;

namespace AppointmentsApi.Contracts.Requests;

public class CreateAppointmentRequest
{
    public Guid DoctorId { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public Guid PatientId { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string PatientMiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
    public TimeSpan Begin { get; set; }
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
    public TimeSpan End { get; set; }
    public DateTime Date { get; set; }
    public AppointmentStatus Status { get; set; }
}