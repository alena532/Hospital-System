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
        
        MimeMessage emailMessage;
        if (mailRequest is MailForPatientRegistrationRequest patientMessage)
        {
            emailMessage = await CreateMailForPatientRegistrationMessage(patientMessage);
        }
        else
        {
            emailMessage = CreateMailForDoctorConfirmationMessage((MailForDoctorConfirmationRequest)mailRequest);
        }
        await SendAsync(emailMessage);
    }

    public async Task VerifiedEmail(Guid accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId, trackChanges: true);
        account.IsEmailVerified = true;
        await _accountRepository.SaveChangesAsync();
    }

    private async Task<MimeMessage> CreateMailForPatientRegistrationMessage(MailForPatientRegistrationRequest message)
    {
        var emailMessage = new MimeMessage();
        
        var account = await _accountRepository.GetByIdAsync(message.AccountId, trackChanges: false);
        if (account == null)
        {
            throw new BadHttpRequestException("Account doesnt exists");
        }
        
        emailMessage.From.Add(new MailboxAddress("email",_mailSettings.Mail));
        emailMessage.To.Add(MailboxAddress.Parse(message.ToEmail));
        emailMessage.Subject = "Checking email";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<a href='https://localhost:4200/profiles/createProfile/accountId={0}' style='color:black;'>Create profile</a>", message.AccountId) };
        
        return emailMessage;
    }
    
    private MimeMessage CreateMailForDoctorConfirmationMessage(MailForDoctorConfirmationRequest message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("email",_mailSettings.Mail));
        emailMessage.To.Add(MailboxAddress.Parse(message.ToEmail));
        emailMessage.Subject = "Confirmation email";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<p>Hello,{0} {1} {2} </p> <a href='https://localhost:1234?doctorId={3}' style='color:black;'>Confirm profile</a>", message.FirstName,message.LastName,message.MiddleName,message.AccountId) };
        
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