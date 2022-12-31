namespace AuthApi.Contracts.Responses;

public class AuthenticatedResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public UserCredentialsResponse User { get; set; }
}