using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Orchestrator.Common.Attributes;
using Orchestrator.Mappers;
using Orchestrator.Services.Implementations;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Extensions;

public static class ServiceExtensions
{
    
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper( typeof(DoctorProfilesMapper),typeof(PatientProfilesMapper),typeof(ReceptionistProfilesMapper));
    }
    
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IDoctorProfilesService,DoctorProfilesService>();
        services.AddTransient<IPatientProfilesService,PatientProfilesService>();
        services.AddTransient<IReceptionistProfilesService,ReceptionistProfilesService>();
    }

    public static void ConfigureFilters(this IServiceCollection services)
    {
        
    }
    
}