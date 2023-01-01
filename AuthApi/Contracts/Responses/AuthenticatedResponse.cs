namespace AuthApi.Contracts.Responses;

public class AuthenticatedResponse
{
    public TokensResponse Tokens { get; set; }
    public UserCredentialsResponse User { get; set; }
}