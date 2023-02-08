using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsApi.DataAccess.Models;

public class PatientAppointment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public PatientAppointment()
    {
        Appointments = new HashSet<Appointment>();
    }
   
    public virtual ICollection<Appointment> Appointments { get; set; }

    public Guid PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
}