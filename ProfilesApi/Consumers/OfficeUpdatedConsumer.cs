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
    private readonly IDoctorProfilesService _service;
    private readonly IDoctorProfileRepository _repository;
    private readonly ILogger<OfficeUpdatedConsumer> _logger;

    public OfficeUpdatedConsumer(IDoctorProfilesService service,IDoctorProfileRepository repository,ILogger<OfficeUpdatedConsumer> logger)
  {
      _service = service;
      _repository = repository;
      _logger = logger;
  }

  public async Task Consume(ConsumeContext<IOfficeUpdated> context)
  {
      var offices = await _service.GetAllByOfficeIdAsync(context.Message.Id,trackChanges:true);
      
      foreach (var office in offices)
      {
          office.Address = context.Message.Address;
      }

      await _repository.SaveChangesAsync();


  }
}