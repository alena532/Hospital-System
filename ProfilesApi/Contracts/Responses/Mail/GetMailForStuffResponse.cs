using ProfilesApi.Contracts.Requests.Mail;

namespace ProfilesApi.Contracts.Mail;

public class GetMailForStuffResponse:MailResponse
{
    public string Password { get; set; }
}