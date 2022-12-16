using FluentValidation;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.DataAccess.Models;


namespace OfficesApi.Validators;

public class CreateOfficeRequestValidator : AbstractValidator<CreateOfficeRequest> 
{
    public CreateOfficeRequestValidator()
    {
        RuleFor(x => x.RegistryPhoneNumber).Matches("^\\+");
        RuleFor(x => x.Status).Must(status => status == OfficeStatus.Active || status == OfficeStatus.InActive);


    }
}