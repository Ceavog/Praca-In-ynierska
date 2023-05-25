namespace Backend.Services;

public class JwtConfiguration : IJwtConfiguration
{
    public string Key { get; set; }
    public string Issuer { get; set; }
}