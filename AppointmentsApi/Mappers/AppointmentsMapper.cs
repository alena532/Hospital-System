using AppointmentsApi.Contracts.Requests;
using AppointmentsApi.Contracts.Responses;
using AppointmentsApi.DataAccess.Models;
using AutoMapper;

namespace AppointmentsApi.Mappers;

public class AppointmentsMapper:Profile
{
    public AppointmentsMapper()
    {
        CreateMap<Appointment, GetFreeTimeForAppointmentResponse>();
        CreateMap<CreateAppointmentRequest, Appointment>();
        CreateMap<Appointment, GetAppointmentResponse>()
            .ForMember(dest => dest.DoctorFirstName,
                opt => opt.MapFrom(x => x.DoctorAppointment.FirstName))
            .ForMember(dest => dest.DoctorLastName,
                opt => opt.MapFrom(x =>  x.DoctorAppointment.LastName))
            .ForMember(dest => dest.DoctorMiddleName,
                opt => opt.MapFrom(x =>  x.DoctorAppointment.MiddleName))
            .ForMember(dest => dest.PatientFirstName,
                opt => opt.MapFrom(x => x.PatientAppointment.FirstName))
            .ForMember(dest => dest.PatientLastName,
                opt => opt.MapFrom(x =>  x.PatientAppointment.LastName))
            .ForMember(dest => dest.PatientMiddleName,
                opt => opt.MapFrom(x =>  x.PatientAppointment.MiddleName))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(x =>  x.PatientAppointment.PhoneNumber));



       
    }
}