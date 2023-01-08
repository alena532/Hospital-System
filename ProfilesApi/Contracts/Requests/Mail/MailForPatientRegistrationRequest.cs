using System.Text.Json.Serialization;
using ProfilesApi.Common;

namespace ProfilesApi.Contracts.Requests.Mail;

public class MailForPatientRegistrationRequest : MailRequest
{
    //[JsonConverter(typeof(GuidJsonConverter))]
    public Guid AccountId { get; set; }
}