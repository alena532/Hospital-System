using AuthApi.DataAccess;
using AuthApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services.Implementations;

public class UsersService : IUsersService
{
    private readonly AppDbContext _context;
    public UsersService(AppDbContext context)
    {
        _context = context;
    }
    

    public async Task DeleteAsync(Guid id)
    {
        var user =  await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new BadHttpRequestException("User doesnt found");
        }

        _context.Users.Remove(user);
    }
       

    
}