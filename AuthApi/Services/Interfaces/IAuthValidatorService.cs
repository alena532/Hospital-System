namespace AuthApi.Services.Interfaces;

public interface IAuthValidatorService
{
    void ValidateEmailAsync(string email);
}