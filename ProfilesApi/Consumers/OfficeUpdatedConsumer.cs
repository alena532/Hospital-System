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
    private readonly IDoctorProfileRepository _repository;
    private readonly ILogger<OfficeUpdatedConsumer> _logger;

    public OfficeUpdatedConsumer(IDoctorProfileRepository repository,ILogger<OfficeUpdatedConsumer> logger)
  {
      _repository = repository;
      _logger = logger;
  }

  public async Task Consume(ConsumeContext<IOfficeUpdated> context)
  {
      var offices = await _repository.GetAllByOfficeIdAsync(context.Message.Id, trackChanges: true);

      foreach (var office in offices)
      {
          office.Address = context.Message.Address;
      }

      _repository.SaveChangesAsync();
      await _repository.SaveChangesAsync();


  }
}