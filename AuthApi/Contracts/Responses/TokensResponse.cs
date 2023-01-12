namespace AuthApi.Contracts.Responses;

public class TokensResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}