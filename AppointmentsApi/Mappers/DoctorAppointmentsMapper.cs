using AppointmentsApi.Contracts.Requests;
using AppointmentsApi.Contracts.Responses;
using AppointmentsApi.DataAccess.Models;
using AutoMapper;

namespace AppointmentsApi.Mappers;

public class DoctorAppointmentsMapper:Profile
{
    public DoctorAppointmentsMapper()
    {
        CreateMap<CreateAppointmentRequest, DoctorAppointment>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(x => x.DoctorFirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(x => x.DoctorLastName))
            .ForMember(dest => dest.MiddleName,
                opt => opt.MapFrom(x => x.DoctorMiddleName));


    }
}