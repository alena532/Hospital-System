namespace AuthApi.Contracts.Requests;

public class TokensRequest
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}