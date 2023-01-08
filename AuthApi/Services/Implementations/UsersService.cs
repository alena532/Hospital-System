using AuthApi.DataAccess;
using AuthApi.DataAccess.Repositories.Interfaces;
using AuthApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services.Implementations;

public class UsersService : IUsersService
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _repository;
    
    public UsersService(AppDbContext context,IUserRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
        {
            throw new BadHttpRequestException("User doesnt found");
        }

        _repository.DeleteAsync(user);
    }
       

    
}