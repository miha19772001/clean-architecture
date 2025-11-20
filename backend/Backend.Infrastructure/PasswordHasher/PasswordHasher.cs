namespace Backend.Infrastructure.PasswordHasher;

using BCrypt.Net;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) =>
        BCrypt.EnhancedHashPassword(password, HashType.SHA256, workFactor: 11);

    public bool Verify(string password, string hashedPassword) =>
        BCrypt.EnhancedVerify(password, hashedPassword, hashType: HashType.SHA256);
}