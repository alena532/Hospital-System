using AppointmentsApi.Contracts.Requests;
using AppointmentsApi.DataAccess.Models;
using AutoMapper;

namespace AppointmentsApi.Mappers;

public class PatientAppointmentsMapper:Profile
{
    public PatientAppointmentsMapper()
    {
        CreateMap<CreateAppointmentRequest, PatientAppointment>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(x => x.PatientFirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(x => x.PatientLastName))
            .ForMember(dest => dest.MiddleName,
                opt => opt.MapFrom(x => x.PatientMiddleName));


    }
}