namespace Backend.Domain.AggregatesModel.UserAggregate;

using System;
using Common;
using Errors;

public class User : AggregateRoot
{
    [Obsolete("Only for EF", true)]
    public User()
    {
    }

    private User(string email, string passwordHash)
    {
        Email = email.Trim();
        PasswordHash = passwordHash;
        Role = Role.User;
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public Role Role { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public static User Create(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw DomainErrors.General.ValueIsRequired(nameof(Email));

        if (string.IsNullOrWhiteSpace(password))
            throw DomainErrors.General.ValueIsRequired(nameof(PasswordHash));

        return new User(email, password);
    }
}