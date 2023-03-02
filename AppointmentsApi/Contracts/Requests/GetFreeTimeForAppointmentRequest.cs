namespace AppointmentsApi.Contracts.Requests;

public class GetFreeTimeForAppointmentRequest
{
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public int Minutes { get; set; }
}