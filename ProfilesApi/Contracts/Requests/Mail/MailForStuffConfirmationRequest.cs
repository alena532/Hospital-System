namespace ProfilesApi.Contracts.Requests.Mail;

public class MailForStuffConfirmationRequest : MailRequest
{
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Password { get; set; }
}