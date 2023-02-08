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
    
    [HttpPost("SendToStuff")]
    public async Task<IActionResult> SendToStuffMail([FromBody]MailForStuffConfirmationRequest request)
    {
        await _service.SendEmailAsync(request);
        return Ok();
    }
    
    [HttpPost("SendToPatient")]
    public async Task<IActionResult> SendToPatientMail([FromBody]MailForPatientRegistrationRequest request)
    {
        await _service.SendEmailAsync(request);
        return Ok();
    }
    
    [HttpPost("Verified")]
    public IActionResult SetEmailVerified([FromBody]Guid accountId)
    {
        _service.VerifiedEmail(accountId);
        return Ok();
    }
    
    
}