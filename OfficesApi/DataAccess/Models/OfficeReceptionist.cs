using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace OfficesApi.DataAccess.Models;

public class OfficeReceptionist
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid ReceptionistId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public Guid OfficeId { get; set; }
    public virtual Office Office { get; set; }
}

