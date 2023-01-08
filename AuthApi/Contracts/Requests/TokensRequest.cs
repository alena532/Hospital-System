namespace AuthApi.Contracts.Requests;

public class TokensRequest
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}