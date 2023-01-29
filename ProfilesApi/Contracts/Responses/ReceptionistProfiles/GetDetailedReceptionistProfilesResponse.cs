namespace ProfilesApi.Contracts.ReceptionistProfiles;

public class GetDetailedReceptionistProfilesResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public Guid AccountId { get; set; }
    public Guid OfficeId { get; set; }
    public string Address { get; set; }
}