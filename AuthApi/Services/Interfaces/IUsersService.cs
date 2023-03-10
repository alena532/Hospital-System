namespace AuthApi.Services.Interfaces;

public interface IUsersService
{
    Task DeleteAsync(Guid id);
}