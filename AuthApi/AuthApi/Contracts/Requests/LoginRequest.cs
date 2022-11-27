using System.ComponentModel.DataAnnotations;

namespace AuthApi.Contracts.Requests;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}