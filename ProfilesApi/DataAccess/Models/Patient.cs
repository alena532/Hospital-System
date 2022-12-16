namespace ProfilesApi.DataAccess.Models;

public class Patient
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Guid PhotoId { get; set; }
    public string Url { get; set; }
    public bool IsLinkedToAccount { get; set; }
    public virtual Account Account { get; set; }
}