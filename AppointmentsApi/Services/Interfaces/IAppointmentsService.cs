using AppointmentsApi.Contracts.Requests;
using AppointmentsApi.Contracts.Responses;

namespace AppointmentsApi.Services.Interfaces;

public interface IAppointmentsService
{
    Task<GetAppointmentResponse> CreateAsync(CreateAppointmentRequest request);
    Task<IEnumerable<GetFreeTimeForAppointmentResponse>> GetFreeTimeForAppointmentAsync(GetFreeTimeForAppointmentRequest request);
    Task<IEnumerable<GetAppointmentResponse>> GetAllAsync();
}