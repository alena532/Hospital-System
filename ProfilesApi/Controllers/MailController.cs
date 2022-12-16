using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class MailController : Controller
{
    private readonly IMailService _service;
    
    public MailController(IMailService mailService)
    {
        this._service = mailService;
    }
    
    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromBody]MailRequest request)
    {
        await _service.SendEmailAsync(request);
        return Ok();
    }
    
    [HttpPost("verified")]
    public IActionResult VerifiedEmail([FromBody]Guid accountId)
    {
        _service.VerifiedEmail(accountId);
        return Ok();
    }
    
    
}