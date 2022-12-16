using FluentValidation;
using OfficesApi.Contracts.Requests.Offices;
using OfficesApi.DataAccess.Models;

namespace OfficesApi.Validators;

public class UpdateOfficeRequestValidator: AbstractValidator<EditOfficeRequest> 
{
    public UpdateOfficeRequestValidator()
    {
        RuleFor(x => x.RegistryPhoneNumber).Matches("^\\+");
        RuleFor(x => x.Status).Must(status => status == OfficeStatus.Active || status == OfficeStatus.InActive);


    }
}