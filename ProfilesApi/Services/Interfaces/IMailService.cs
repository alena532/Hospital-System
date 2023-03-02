using ProfilesApi.Contracts.Mail;
using ProfilesApi.Contracts.Requests.Mail;

namespace ProfilesApi.Services.Interfaces;

public interface IMailService
{
    Task<MailResponse> SendEmailAsync(MailRequest mailRequest);
    Task VerifiedEmail(Guid accountId);

}