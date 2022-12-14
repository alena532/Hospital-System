using FluentValidation;
using OfficesApi.Contracts.Requests.OfficeReceptionist;

namespace OfficesApi.Validators;

public class UpdateOfficeReceptionistValidator : AbstractValidator<EditOfficeReceptionistRequest> 
{
    public UpdateOfficeReceptionistValidator()
    {
        RuleFor(x => x.PhotoUrl).Matches("^https");
        
    }
}