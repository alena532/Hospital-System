using FluentValidation;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.DataAccess.Models;

namespace ProfilesApi.Validators;

public class EditDoctorProfileRequestValidator : AbstractValidator<EditDoctorProfileRequest>
{
    public EditDoctorProfileRequestValidator()
    { 
        RuleFor(profile => profile.FirstName).NotNull().NotEmpty().Length(1,40);
        RuleFor(profile => profile.LastName).NotNull().NotEmpty().Length(1, 50);
        RuleFor(profile => profile.Status).NotNull().Must(status => status == DoctorStatusEnum.Inactive || status == DoctorStatusEnum.AtWork || status == DoctorStatusEnum.OnVacation || status == DoctorStatusEnum.SelfIsolation || status == DoctorStatusEnum.SickDay || status == DoctorStatusEnum.SickLeave || status == DoctorStatusEnum.LeaveWithoutPay);
        RuleFor(profile => profile.CareerStartYear).NotEmpty().Must(year => year <= DateTime.Now.Year && year >= 1920);
        RuleFor(profile => profile.DateOfBirth).NotNull().NotEmpty().Must(date => DateTime.Now.Year - date.Year >= 18);
        
    }
}