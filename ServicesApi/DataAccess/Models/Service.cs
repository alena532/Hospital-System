using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApi.DataAccess.Models;

public class Service
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid ServiceCategoryId { get; set; }
    public virtual ServiceCategory ServiceCategory { get; set; }
    public Guid? SpecializationId { get; set; }
    public virtual Specialization Specialization { get; set; }
    public string ServiceName { get; set; }
    public int Price { get; set; }
    public ServiceStatus Status { get; set; }
}