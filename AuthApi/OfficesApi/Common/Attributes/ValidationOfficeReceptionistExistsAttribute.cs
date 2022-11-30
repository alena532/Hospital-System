using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficesApi.DataAccess.Repositories.Interfaces;

namespace OfficesApi.Common.Attributes;

public class ValidationOfficeReceptionistExistsAttribute : IAsyncActionFilter
{
    private readonly IOfficeReceptionistRepository _receptionistRepository;
    private readonly IOfficeRepository _officeRepository;

    public ValidationOfficeReceptionistExistsAttribute(IOfficeReceptionistRepository receptionistRepository,IOfficeRepository officeRepository)
    {
        _receptionistRepository = receptionistRepository;
        _officeRepository = officeRepository;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
        var officeId = (int)context.ActionArguments["officeId"];
        
        var office = await _officeRepository.GetOfficeAsync(officeId, trackChanges: false);

        if (office == null)
        {
            context.Result = new NotFoundResult();
        }
        
        var receptionistId = (int)context.ActionArguments["id"];

        var receptionist = await _receptionistRepository.GetOfficeReceptionistAsync(officeId,receptionistId, trackChanges: false);

        if (receptionist == null)
        {
            context.Result = new NotFoundResult();
        }
        
        else
        {
            context.HttpContext.Items.Add("receptionist", receptionist);
            await next();
        }
    }
}
