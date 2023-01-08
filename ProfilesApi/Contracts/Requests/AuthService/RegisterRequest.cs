using System.ComponentModel.DataAnnotations;

namespace ProfilesApi.Contracts.Requests;

public class RegisterRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid RoleId { get; set; }
}