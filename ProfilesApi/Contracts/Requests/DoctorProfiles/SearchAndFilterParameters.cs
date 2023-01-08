namespace ProfilesApi.Contracts;

public class SearchAndFilterParameters
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid? OfficeId { get; set; }
}