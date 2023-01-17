using FluentValidation;
using ProfilesApi.Contracts.Requests.PatientProfiles;

namespace ProfilesApi.Validators;

public class CreatePatientProfileRequestValidator : AbstractValidator<CreatePatientProfileRequest>
{
    public CreatePatientProfileRequestValidator()
    { 
        RuleFor(profile => profile.FirstName).NotNull().NotEmpty().Length(1,40);
        RuleFor(profile => profile.LastName).NotNull().NotEmpty().Length(1, 50);
        RuleFor(x => x.PhoneNumber).Matches("^\\+");
        RuleFor(profile => profile.DateOfBirth).NotNull().NotEmpty().Must(date => DateTime.Now.Year - date.Year >= 18);
        
    }
}