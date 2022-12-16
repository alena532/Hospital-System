namespace ProfilesApi.DataAccess.Models;

public class Doctor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int CareerStartYear { get; set; }
    public DoctorStatusEnum Status { get; set; }
    
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
    
    public string Address { get; set; }
    public Guid OfficeId { get; set; }
    public Guid AccountId { get; set; }
    
    public Guid PhotoId { get; set; }
    public string Url { get; set; }
    
    public virtual Account Account { get; set; }
}