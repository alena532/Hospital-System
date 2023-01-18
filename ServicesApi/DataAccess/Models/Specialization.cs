using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApi.DataAccess.Models;

public class Specialization
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string SpecializationName { get; set; }
    public SpecializationStatus Status { get; set; }
    public Specialization()
    {
        Services = new HashSet<Service>();
    }
    public virtual ICollection<Service> Services { get; set; }
}