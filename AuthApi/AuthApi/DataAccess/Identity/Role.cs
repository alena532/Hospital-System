using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.DataAccess;

public class Role:IdentityRole<string>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public Role()
    {
        Users = new HashSet<User>();
    }
    public virtual ICollection<User> Users { get; set; }
    
}