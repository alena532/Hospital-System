using ProfilesApi.Contracts.Requests.Mail;

namespace ProfilesApi.Services.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
    Task VerifiedEmail(Guid accountId);

}