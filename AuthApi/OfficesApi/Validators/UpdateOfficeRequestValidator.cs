using FluentValidation;
using OfficesApi.Contracts.Requests.Offices;

namespace OfficesApi.Validators;

public class UpdateOfficeRequestValidator: AbstractValidator<EditOfficeRequest> 
{
    public UpdateOfficeRequestValidator()
    {
        RuleFor(x => x.RegistryPhoneNumber).Matches("^\\+");
        RuleFor(x => x.Status).Must(x => (int) x >= 0 && (int) x <= 2);


    }
}