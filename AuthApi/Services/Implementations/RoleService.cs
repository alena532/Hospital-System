using AuthApi.DataAccess;
using AuthApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services.Implementations;

public class RoleService:IRoleService
{
    private readonly AppDbContext _context;
    public RoleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> GetByNameAsync(string name)
        => await _context.Roles.Where(x => x.Name.Equals(name)).Select(x => x.Id).SingleOrDefaultAsync();

}