using AuthApi.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController] 
[Authorize(Roles="Doctor")]
[Route("api/doctor/[controller]")]
public class TestController : Controller
{
    private readonly AppDbContext _context;
    
    public TestController(AppDbContext context)
    {
        _context = context;
    }
    
    
}