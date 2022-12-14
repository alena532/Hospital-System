using FluentValidation;
using ProfilesApi.Contracts.Requests.DoctorProfiles;

namespace ProfilesApi.Validators;

public class CreateDoctorProfileRequestValidator : AbstractValidator<CreateDoctorProfileRequest>
{
    
    public CreateDoctorProfileRequestValidator()
    { 
        RuleFor(profile => profile.FirstName).NotNull().NotEmpty().Length(1,40);
        RuleFor(profile => profile.LastName).NotNull().NotEmpty().Length(1, 50);
        RuleFor(profile => profile.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(profile => profile.SpecializationName).NotNull().NotEmpty();
        RuleFor(profile => profile.Address).NotNull().NotEmpty();
        RuleFor(profile => profile.OfficeId).NotNull().NotEmpty();
        RuleFor(profile => profile.PhoneNumber).NotNull().NotEmpty();
        RuleFor(profile => profile.Status).NotNull().NotEmpty().Must(status => (int) status >= 0 && (int) status <= 6);
        RuleFor(profile => profile.CareerStartYear).NotNull().NotEmpty().Must(year => year <= DateTime.Now.Year);
        RuleFor(profile => profile.DateOfBirth).NotNull().NotEmpty().Must(date => date.CompareTo(DateOnly.FromDateTime(DateTime.Now)) == -1);
        
    }
}