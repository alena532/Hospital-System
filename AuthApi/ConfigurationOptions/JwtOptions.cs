namespace AuthApi.ConfigurationOptions;

public class JwtOptions
{
    public const string Path = "Jwt";

    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Expiration { get; set; }
    
}