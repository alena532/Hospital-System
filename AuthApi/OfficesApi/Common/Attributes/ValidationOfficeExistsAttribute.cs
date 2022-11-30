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
    //private readonly IValidator<Office> _validator;
    
    public ValidationOfficeExistsAttribute(IOfficeRepository repository)
    {
        _repository = repository;
        //_validator = validator;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
        var id = (Guid)context.ActionArguments["id"];
        var office = await _repository.GetOfficeAsync(id, trackChanges);

        if (office == null)
        {
            context.Result = new NotFoundResult();
        }
        else
        {
           // ValidationResult result = await _validator.ValidateAsync(office);
            //if (!result.IsValid)
            //{

              //  context.Result = new ValidationException(result.Errors.ToArray());
           // }

            context.HttpContext.Items.Add("office", office);
            await next();
        }
    }
}