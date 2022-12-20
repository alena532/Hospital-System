namespace AuthApi.Services.Interfaces;

public interface IAuthValidatorService
{
    Task ValidateEmailAsync(string email);
}