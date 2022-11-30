using FluentValidation;
using OfficesApi.Contracts.Requests.OfficeReceptionist;

namespace OfficesApi.Validators;

public class CreateOfficeReceptionistValidator : AbstractValidator<CreateOfficeReceptionistRequest> 
{
    public CreateOfficeReceptionistValidator()
    {
        RuleFor(x => x.PhotoUrl).Matches("^\\https");
        
    }
}