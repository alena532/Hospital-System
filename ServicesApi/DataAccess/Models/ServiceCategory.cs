using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApi.DataAccess.Models;

public class ServiceCategory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string CategoryName { get; set; }
    public int TimeSlotSize { get; set; }
    public ServiceCategory()
    {
        Services = new HashSet<Service>();
    }
    public virtual ICollection<Service> Services { get; set; }
}