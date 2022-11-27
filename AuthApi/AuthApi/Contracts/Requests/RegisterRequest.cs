using System.ComponentModel.DataAnnotations;

namespace AuthApi.Contracts.Requests;

public class RegisterRequest
{
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
    public string RoleId { get; set; }
}