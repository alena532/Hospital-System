using System.ComponentModel.DataAnnotations;

namespace ProfilesApi.Contracts.Requests.Mail;

public class MailRequest
{
    [EmailAddress]
    public string ToEmail { get; set; }
    public Guid AccountId { get; set; }
}