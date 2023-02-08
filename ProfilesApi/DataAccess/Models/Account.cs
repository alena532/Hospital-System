namespace ProfilesApi.DataAccess.Models;

public class Account
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsEmailVerified { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set;}
    public DateTime UpdateAt { get; set; }
    public virtual Doctor Doctor { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual Receptionist Receptionist { get; set; }
}