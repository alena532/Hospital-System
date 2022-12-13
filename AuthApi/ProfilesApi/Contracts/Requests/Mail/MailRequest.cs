namespace ProfilesApi.Contracts.Requests.Mail;

public class MailRequest
{
    public string ToEmail { get; set; }
    public Guid AccountId { get; set; }
}