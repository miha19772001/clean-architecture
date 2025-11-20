namespace Backend.Infrastructure.JwtProvider;

public class JwtOptions
{
    public string SecretKey { get; init; } = string.Empty;
    public int ExpiresDays { get; init; }
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}