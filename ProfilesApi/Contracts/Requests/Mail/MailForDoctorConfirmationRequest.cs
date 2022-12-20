namespace ProfilesApi.Contracts.Requests.Mail;

public class MailForDoctorConfirmationRequest : MailRequest
{
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
}