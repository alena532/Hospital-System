namespace AuthApi.Contracts.Responses;

public class UserCredentialsResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}