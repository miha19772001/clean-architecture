namespace Backend.Domain.AggregatesModel.UserAggregate;

using Common;
using Errors;
using System.Collections.Generic;

public class Role : ValueObject
{
    public static readonly Role User = new(nameof(User));
    public static readonly Role Admin = new(nameof(Admin));

    private static readonly Role[] Roles = [User, Admin];

    private Role(string value)
    {
        Value = value;
    }

    public string Value { get; }
    
    public static Role Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw DomainErrors.General.ValueIsRequired(nameof(Role));

        var role = value.Trim().ToLower();

        var hasValue =
            Roles.Any(x => x.Value.Equals(role, StringComparison.CurrentCultureIgnoreCase));

        if (!hasValue)
            throw DomainErrors.General.ValueIsInvalid(nameof(Role));

        return new Role(role);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}