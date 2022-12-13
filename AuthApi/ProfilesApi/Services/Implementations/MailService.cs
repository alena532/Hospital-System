using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ProfilesApi.Common.Settings;
using ProfilesApi.Contracts.Requests.Mail;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Services.Interfaces;

namespace ProfilesApi.Services.Implementations;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly ILogger<MailService> _logger;
    private readonly IAccountRepository _accountRepository;
    
    public MailService(IOptions<MailSettings> mailSettings,ILogger<MailService> logger,IAccountRepository accountRepository)
    {
        _mailSettings = mailSettings.Value;
        _logger = logger;
        _accountRepository = accountRepository;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var emailMessage = CreateEmailMessage(mailRequest);

        await SendAsync(emailMessage);
        
    }
    
    private MimeMessage CreateEmailMessage(MailRequest message)
    {
        var emailMessage = new MimeMessage();
        var account = _accountRepository.GetAccountById(message.AccountId, trackChanges: false);
        if (account == null)
        {
            throw new BadHttpRequestException("Account doesnt exists");
        }
        emailMessage.From.Add(new MailboxAddress("email",_mailSettings.Mail));
        emailMessage.To.Add(MailboxAddress.Parse(message.ToEmail));
        emailMessage.Subject = "Checking email";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<a href='https://localhost:1234?accountId={0}' style='color:black;'>Create profile</a>", message.AccountId) };
        
        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

                await client.SendAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"send email failed {ex.Message}");
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}