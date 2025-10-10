namespace Backend.Domain.AggregatesModel.UserAggregate;

using Common;
using Errors;
using System.Collections.Generic;

public class Role : ValueObject
{
    public static readonly Role User = new(nameof(User));
    public static readonly Role Admin = new(nameof(Admin));

    private static readonly Role[] Roles = [User, Admin];

    public string Value { get; private set; }

    private Role(string value)
    {
        Value = value;
    }

    public static Role Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw DomainErrors.General.ValueIsRequired(nameof(Role));

        var role = input.Trim().ToLower();

        var isRolePresent =
            Roles.Any(ft => ft.Value.Equals(role, StringComparison.CurrentCultureIgnoreCase));

        if (!isRolePresent)
            throw DomainErrors.General.ValueIsInvalid(nameof(Role));

        return new Role(role);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}