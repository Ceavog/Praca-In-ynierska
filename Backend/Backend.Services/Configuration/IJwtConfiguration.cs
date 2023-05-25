namespace Backend.Services;

public interface IJwtConfiguration
{
    string Key { get; set; }
    string Issuer { get; set; }
}