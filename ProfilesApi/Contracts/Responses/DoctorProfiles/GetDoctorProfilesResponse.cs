namespace ProfilesApi.Contracts.Responses.DoctorProfiles;

public class GetDoctorProfilesResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Specialization { get; set; }
    public int Experience { get; set; }
    public string Address { get; set; }
    public string Url { get; set; }
}