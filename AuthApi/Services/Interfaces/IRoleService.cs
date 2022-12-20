namespace AuthApi.Services.Interfaces;

public interface IRoleService
{
    Task<Guid> GetByNameAsync(string name);
}