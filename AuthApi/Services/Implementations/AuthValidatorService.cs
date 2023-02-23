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
    public void ValidateEmailAsync(string email)
    {
        var users = _context.Users.Where(x => x.Email == email).ToList();
        if (users.Count != 0)
        {
            throw new BadHttpRequestException("Email already exists");
        }
    }
}