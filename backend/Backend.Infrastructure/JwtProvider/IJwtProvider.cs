namespace Backend.Infrastructure.JwtProvider;

public interface IJwtProvider
{
    string GenerateToken(Guid sessionId);
}