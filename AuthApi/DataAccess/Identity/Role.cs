using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.DataAccess;

public class Role:IdentityRole<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Role()
    {
        Users = new HashSet<User>();
    }
    public virtual ICollection<User> Users { get; set; }
    
}