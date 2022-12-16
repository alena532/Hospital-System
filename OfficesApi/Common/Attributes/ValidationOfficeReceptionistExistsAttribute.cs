using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficesApi.DataAccess.Repositories.Interfaces;

namespace OfficesApi.Common.Attributes;

public class ValidationOfficeReceptionistExistsAttribute : IAsyncActionFilter
{
    private readonly IOfficeReceptionistRepository _receptionistRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly ILogger<ValidationOfficeReceptionistExistsAttribute> _logger;

    public ValidationOfficeReceptionistExistsAttribute(IOfficeReceptionistRepository receptionistRepository,IOfficeRepository officeRepository,ILogger<ValidationOfficeReceptionistExistsAttribute> logger)
    {
        _receptionistRepository = receptionistRepository;
        _officeRepository = officeRepository;
        _logger = logger;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
        var officeId = (Guid)context.ActionArguments["officeId"];
        
        var office = await _officeRepository.GetByIdAsync(officeId, trackChanges: false);

        if (office == null)
        {
            _logger.LogWarning($"Office with id: {officeId} doesn't exist in the database.");
            context.Result = new NotFoundResult();
        }
        
        var receptionistId = (Guid)context.ActionArguments["id"];

        var receptionist = await _receptionistRepository.GetOfficeReceptionistAsync(officeId,receptionistId, trackChanges: false);

        if (receptionist == null)
        {
            _logger.LogWarning($"Receptionist with id: {receptionistId} doesn't exist in the database.");
            context.Result = new NotFoundResult();
        }
        
        else
        {
            context.HttpContext.Items.Add("receptionist", receptionist);
            await next();
        }
    }
}
