namespace ProfilesApi.Contracts.Mail;

public class GetMailAndIdStuffResponse
{
    public GetMailForStuffResponse mailResponse { get; set; }
    public Guid Id { get; set; }
}