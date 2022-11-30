using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace OfficesApi.DataAccess.Models;

public class Office
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public OfficeStatus Status { get; set; }
    
    public Office()
    {
        OfficeReceptionists = new HashSet<OfficeReceptionist>();
    }
    public virtual ICollection<OfficeReceptionist> OfficeReceptionists { get; set; }
}
