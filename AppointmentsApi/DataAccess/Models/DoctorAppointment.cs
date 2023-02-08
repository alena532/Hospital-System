using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsApi.DataAccess.Models;

public class DoctorAppointment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DoctorAppointment()
    {
        Appointments = new HashSet<Appointment>();
    }
   
    public virtual ICollection<Appointment> Appointments { get; set; }

    public Guid DoctorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    
}