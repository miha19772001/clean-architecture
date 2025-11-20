namespace Backend.API.Filters;

using Application.Queries.User;
using Application.Services.Account;
using Domain.AggregatesModel.UserAggregate;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RoleFilterAttribute : Attribute, IAsyncActionFilter
{
    private readonly Role? _requiredRole;


    public RoleFilterAttribute()
    {
        _requiredRole = null;
    }

    public RoleFilterAttribute(RequiredRole role)
    {
        _requiredRole = role switch
        {
            RequiredRole.User => Role.User,
            RequiredRole.Admin => Role.Admin,
            _ => throw new ArgumentException($"Unsupported role: {role}", nameof(role))
        };
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (_requiredRole is null)
        {
            await next();
            return;
        }

        var httpContext = context.HttpContext;
        var ct = httpContext.RequestAborted;

        try
        {
            var accountService = httpContext.RequestServices.GetRequiredService<IAccountService>();
            var mediator = httpContext.RequestServices.GetRequiredService<IMediator>();

            var userId = await accountService.GetCurrentUserIdAsync(ct);

            if (userId == Guid.Empty)
                throw DomainErrors.User.NoRights();

            var user = await mediator.Send(new FindUserByIdQuery(userId), ct);

            if (user is null)
                throw DomainErrors.User.NoRights();

            if (IsAuthorized(user.Role, _requiredRole))
            {
                await next();
            }
            else
            {
                throw DomainErrors.User.NoRights();
            }
        }
        catch (Exception)
        {
            throw DomainErrors.User.NoRights();
        }
    }


    private static readonly Dictionary<string, HashSet<string>> Permissions = new()
    {
        [Role.User.Value] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { Role.User.Value, Role.Admin.Value },

        [Role.Admin.Value] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { Role.Admin.Value }
    };

    private static bool IsAuthorized(string userRole, Role requiredRole)
    {
        if (!Permissions.TryGetValue(requiredRole.Value, out var allowedRoles))
            return false;

        return allowedRoles.Contains(userRole);
    }
}

public enum RequiredRole
{
    User,
    Admin
}