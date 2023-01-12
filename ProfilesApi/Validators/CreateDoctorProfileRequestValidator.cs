using FluentValidation;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.DataAccess.Models;

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
        RuleFor(x => x.PhoneNumber).Matches("^\\+");
        RuleFor(profile => profile.Status).NotNull().Must(status =>status == DoctorStatusEnum.Inactive || status == DoctorStatusEnum.AtWork || status == DoctorStatusEnum.OnVacation || status == DoctorStatusEnum.SelfIsolation || status == DoctorStatusEnum.SickDay || status == DoctorStatusEnum.SickLeave || status == DoctorStatusEnum.LeaveWithoutPay);
        RuleFor(profile => profile.CareerStartYear).NotEmpty().Must(year => year.Year <= DateTime.Now.Year && year.Year >= 1920);
        RuleFor(profile => profile.DateOfBirth).NotNull().NotEmpty().Must(date => date.CompareTo(DateTime.Now) == -1);
        
    }
}