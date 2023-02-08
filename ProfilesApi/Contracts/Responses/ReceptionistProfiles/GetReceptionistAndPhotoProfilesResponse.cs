namespace ProfilesApi.Contracts.ReceptionistProfiles;

public class GetReceptionistAndPhotoProfilesResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string Address { get; set; }
    public byte[] Photo { get; set; }
}