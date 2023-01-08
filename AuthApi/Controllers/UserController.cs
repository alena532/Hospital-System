using AuthApi.DataAccess;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController] 

[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUsersService _service;
    public UserController(IUsersService service)
    {
        _service = service;
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
   
    
    
}