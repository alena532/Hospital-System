using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;
    public RoleController(IRoleService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<Guid>> GetByName([FromQuery] string name)
        =>Ok(await _service.GetByNameAsync(name));
    
}