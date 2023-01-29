namespace ProfilesApi.Contracts.Requests.ReceptionistProfiles;

public class CreateReceptionistProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid OfficeId { get; set; }
    public string Address { get; set; }
}