using AuthApi.DataAccess;
using AuthApi.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services.Implementations;

public class AuthValidatorService: IAuthValidatorService
{
    private readonly AppDbContext _context;
    public AuthValidatorService(AppDbContext context)
    {
        _context = context;
    }
    public async Task ValidateEmailAsync(string email)
    {
        var users = await _context.Users.Where(x => x.Email == email).ToListAsync();
        if (users.Count != 0)
        {
            throw new BadHttpRequestException("Email already exists");
        }
    }
}