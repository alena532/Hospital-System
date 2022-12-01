using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace OfficesApi.DataAccess.Models;

public class OfficeReceptionist
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ReceptionistId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public int PhotoId { get; set; }
    public string PhotoUrl { get; set; }
    
    
    public int OfficeId { get; set; }
    public virtual Office Office { get; set; }
}

