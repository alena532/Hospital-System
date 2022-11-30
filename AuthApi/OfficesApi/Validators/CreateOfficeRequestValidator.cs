using FluentValidation;
using OfficesApi.Contracts.Requests.Offices;


namespace OfficesApi.Validators;

public class CreateOfficeRequestValidator : AbstractValidator<CreateOfficeRequest> 
{
    public CreateOfficeRequestValidator()
    {
        RuleFor(x => x.RegistryPhoneNumber).Matches("^\\+");


    }
}