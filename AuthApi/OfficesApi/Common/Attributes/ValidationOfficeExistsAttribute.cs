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

    public ValidationOfficeExistsAttribute(IOfficeRepository repository)
    {
        _repository = repository;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
        var id = (int)context.ActionArguments["id"];
        var office = await _repository.GetOfficeAsync(id, trackChanges);

        if (office == null)
        {
            context.Result = new NotFoundResult();
        }
        else
        {
            context.HttpContext.Items.Add("office", office);
            await next();
        }
    }
}