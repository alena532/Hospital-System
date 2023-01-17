using System.Text.Json.Serialization;
using ProfilesApi.Common;

namespace ProfilesApi.Contracts.Requests.Mail;

public class MailForPatientRegistrationRequest : MailRequest
{
    public Guid AccountId { get; set; }
}