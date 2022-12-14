using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficesApi.Contracts.Responses;

namespace OfficesApi.Common.Attributes;

public class ValidationModelAttribute:IAsyncActionFilter
{
    private readonly ILogger<ValidationOfficeExistsAttribute> _logger;

    public ValidationModelAttribute(ILogger<ValidationOfficeExistsAttribute> logger)
    {
        _logger = logger;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            var errorResponse = new ErrorResponse();

            foreach (var error in errorsInModelState)
            {
                foreach(var suberror in error.Value)
                {
                    var errorModel = new ErrorModel()
                    {
                        FieldName = error.Key,
                        Message = suberror
                    };
                    
                    errorResponse.Errors.Add(errorModel);
                }
                
            }
            _logger.LogWarning("Validation failed");
            context.Result = new BadRequestObjectResult(context.ModelState);
        }

        await next();
    }
}