namespace Backend.Domain.AggregatesModel.UserAggregate;

using System;
using Common;
using Errors;

public class User : AggregateRoot
{
    [Obsolete("Only for EF", true)]
    private User()
    {
    }

    private User(string login, string passwordHash)
    {
        Login = login.Trim();
        PasswordHash = passwordHash;
        Role = Role.User;
        IsDeleted = false;
        CreatedAt = DateTime.Now;
    }

    public string Login { get; private set; }

    public string PasswordHash { get; private set; }

    public Role Role { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public static User Create(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw DomainErrors.General.ValueIsRequired(nameof(Login));

        if (string.IsNullOrWhiteSpace(password))
            throw DomainErrors.General.ValueIsRequired(nameof(PasswordHash));

        return new User(login, password);
    }
}