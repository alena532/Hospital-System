namespace AppointmentsApi.Contracts.Responses;

public class GetFreeTimeForAppointmentResponse
{
    public TimeSpan? Begin { get; set; } = null;
    public TimeSpan? End { get; set; } = null;
}