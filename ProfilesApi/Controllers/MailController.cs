using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesApi.Contracts.Mail;
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
    public async Task<ActionResult<MailResponse>> SendToStuffMail([FromBody]MailForStuffConfirmationRequest request)
    {
        await _service.SendEmailAsync(request);
        return Ok();
    }
    
    [HttpPost("SendToPatient")]
    public async Task<ActionResult<MailResponse>> SendToPatientMail([FromBody]MailForPatientRegistrationRequest request)
    {
        return Ok(await _service.SendEmailAsync(request));
    }
    
    [HttpPost("Verified")]
    public IActionResult SetEmailVerified([FromBody]Guid accountId)
    {
        _service.VerifiedEmail(accountId);
        return Ok();
    }
    
    
}