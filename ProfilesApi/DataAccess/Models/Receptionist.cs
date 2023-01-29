namespace ProfilesApi.DataAccess.Models;

public class Receptionist
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public Guid AccountId { get; set; }
    public string Address { get; set; }
    public Guid OfficeId { get; set; }
    public virtual Account Account { get; set; }
}