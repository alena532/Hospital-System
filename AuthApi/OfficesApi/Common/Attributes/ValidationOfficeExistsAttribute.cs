using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficesApi.DataAccess;
using OfficesApi.DataAccess.Models;
using OfficesApi.DataAccess.Repositories.Interfaces;

namespace OfficesApi.Common.Attributes;

public class ValidationOfficeExistsAttribute : IAsyncActionFilter
{
    private readonly IOfficeRepository _repository;
    private readonly ILogger<ValidationOfficeExistsAttribute> _logger;

    public ValidationOfficeExistsAttribute(IOfficeRepository repository,ILogger<ValidationOfficeExistsAttribute> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        bool inReceptionistController = context.HttpContext.Request.Path.Value.Contains("receptionists");
        
        var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

        Guid id;
        if (inReceptionistController)
        {
            id = (Guid)context.ActionArguments["officeId"];
        }
        else
        {
            id = (Guid)context.ActionArguments["id"];
        }
        
        var office = await _repository.GetOfficeAsync(id, trackChanges);

        if (office == null)
        {
            _logger.LogWarning($"Office with id: {id} doesn't exist in the database.");
            context.Result = new NotFoundResult();
        }
        else
        {
            context.HttpContext.Items.Add("office", office);
            await next();
        }
    }
}