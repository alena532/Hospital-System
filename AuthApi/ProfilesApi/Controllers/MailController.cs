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
    private readonly IMailService mailService;
    public MailController(IMailService mailService)
    {
        this.mailService = mailService;
    }
    [HttpPost("send")]
    public async Task<IActionResult> SendMailAsync([FromForm]MailRequest request)
    {
        try
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
            
    }
}