using AppointmentsApi.Common.Constants;
using AppointmentsApi.Contracts.Requests;
using AppointmentsApi.Contracts.Responses;
using AppointmentsApi.DataAccess.Models;
using AppointmentsApi.DataAccess.Repositories.Implementations;
using AppointmentsApi.DataAccess.Repositories.Interfaces;
using AppointmentsApi.Services.Interfaces;
using AutoMapper;

namespace AppointmentsApi.Services;

public class AppointmentsService:IAppointmentsService
{
    
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorAppointmentRepository _doctorRepository;
    private readonly IPatientAppointmentRepository _patientRepository;
    
    public AppointmentsService(IMapper mapper,IDoctorAppointmentRepository doctorRepository,IPatientAppointmentRepository patientRepository,IAppointmentRepository appointmentRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _appointmentRepository = appointmentRepository;
    }
    
    public async Task<GetAppointmentResponse> CreateAsync(CreateAppointmentRequest request)
    {
        var doctorAppointment = _mapper.Map<DoctorAppointment>(request);
        await _doctorRepository.CreateAsync(doctorAppointment);
        
        var patientAppointment = _mapper.Map<PatientAppointment>(request);
        await _patientRepository.CreateAsync(patientAppointment);

        var appointment =  _mapper.Map<Appointment>(request);
        appointment.DoctorAppointmentId = doctorAppointment.Id;
        appointment.PatientAppointmentId = patientAppointment.Id;
        await _appointmentRepository.CreateAsync(appointment);
        
        return _mapper.Map<GetAppointmentResponse>( await _appointmentRepository.GetByIdAsync(appointment.Id));
    }
    
    public async Task<IEnumerable<GetFreeTimeForAppointmentResponse>> GetFreeTimeForAppointmentAsync(GetFreeTimeForAppointmentRequest request)
    {
        var existing = await GetSortedAppointmentsByDateAndDoctorIdAsync(request.DoctorId, request.Date);
        List<GetFreeTimeForAppointmentResponse> freeTimes = new();
        GetFreeTimeForAppointmentResponse freeTime = new();

        for (var i = ScheduleConstants.Begin; i <= ScheduleConstants.End; i=AddMinutes(i, request.Minutes))
        {
            if (freeTime.Begin != null)
            {
                if (!(existing.Any(x => x.Begin < i && x.End >= i)))
                {
                    freeTime.End = i;
                    freeTimes.Add(freeTime);
                }
                freeTime = new();
            }

            if (!(existing.Any(x => x.Begin <= i && x.End > i)))
            {
                freeTime.Begin = i;
            }
            
        }

        return freeTimes;
    }
    
    public static TimeSpan AddMinutes(TimeSpan timeSpan, int minutesToAdd)
    {
        TimeSpan newSpan = new TimeSpan(0, 0, minutesToAdd, 0);
        return timeSpan.Add(newSpan);
    }
    
    public async Task<List<GetFreeTimeForAppointmentResponse>> GetSortedAppointmentsByDateAndDoctorIdAsync(Guid doctorId,DateTime appointmentDate)
    {
        return _mapper.Map<List<GetFreeTimeForAppointmentResponse>>(await _doctorRepository.GetSortedTimeByDateAndDoctorIdAsync(doctorId, appointmentDate));
    }

    public async Task<IEnumerable<GetAppointmentResponse>> GetAllAsync()
    {
        return _mapper.Map<ICollection<GetAppointmentResponse>>(await _appointmentRepository.GetAllAsync());
    }

    
    
}