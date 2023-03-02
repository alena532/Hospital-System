using MassTransit;
using ProfilesApi.DataAccess;
using ProfilesApi.DataAccess.Repositories.Implementations;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Implementations;
using ProfilesApi.Services.Interfaces;
using SharedModels.Messages;

namespace ProfilesApi.Consumers;

public class OfficeUpdatedConsumer : IConsumer<IOfficeUpdated>
{ 
    private readonly IDoctorProfileRepository _doctorRepository;
    private readonly IReceptionistProfileRepository _receptionistRepository;
    private readonly ILogger<OfficeUpdatedConsumer> _logger;
    
    public OfficeUpdatedConsumer(IDoctorProfileRepository doctorRepository,IReceptionistProfileRepository receptionistRepository,ILogger<OfficeUpdatedConsumer> logger)
    { 
        _doctorRepository = doctorRepository;
        _receptionistRepository = receptionistRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IOfficeUpdated> context)
    {
        var doctors = await _doctorRepository.GetAllByOfficeIdAsync(context.Message.Id, trackChanges: true);
        foreach (var doctor in doctors)
        {
            doctor.Address = context.Message.Address;
        }

        await _doctorRepository.SaveChangesAsync();

        var offices = await _receptionistRepository.GetAllByOfficeIdAsync(context.Message.Id, trackChanges: true);
        foreach (var office in offices)
        {
            office.Address = context.Message.Address;
        }

        await _receptionistRepository.SaveChangesAsync();
    }
}