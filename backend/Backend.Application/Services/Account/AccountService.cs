namespace Backend.Application.Services.Account;

using Infrastructure.Errors;
using Commands.Session;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Queries.Session;
using Domain.Errors;
using Infrastructure.JwtProvider;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public class AccountService(
    IJwtProvider jwtProvider,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator) : IAccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor =
        httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public async Task SignInAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        var realIp = httpContext.Request.Headers["X-Real-IP"];

        if (!string.IsNullOrWhiteSpace(realIp) && IPAddress.TryParse(realIp, out var ipAddress))
        {
            if (ipAddress.IsIPv4MappedToIPv6)
                ipAddress = ipAddress.MapToIPv4();

            ip = ipAddress.ToString();
        }

        var agent = httpContext.Request.Headers.UserAgent.ToString();

        var session = await mediator.Send(
            new CreateSessionCommand(userId, ip, agent),
            cancellationToken);

        var token = jwtProvider.GenerateToken(session.Id);

        httpContext.Response.Cookies.Append("MyShop", token);
    }

    public async Task SignOutAsync(CancellationToken cancellationToken = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var jwtToken = httpContext.Request.Cookies["MyShop"];

        if (string.IsNullOrWhiteSpace(jwtToken))
            throw InfrastructureErrors.General.ErrorMessage("JWT token is not found");

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);

        var sessionIdStr = token.Claims.FirstOrDefault(c => c.Type == "sessionId")?.Value;

        if (string.IsNullOrWhiteSpace(sessionIdStr))
            throw InfrastructureErrors.General.ErrorMessage("SessionId in JWT token is not found");

        var sessionId = Guid.Parse(sessionIdStr);

        await mediator.Send(
            new DeleteSessionCommand(sessionId),
            cancellationToken);

        httpContext.Response.Cookies.Delete("MyShop");
    }

    public async Task<Guid> GetCurrentUserIdAsync(CancellationToken cancellationToken = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var jwtToken = httpContext.Request.Cookies["MyShop"];

        if (string.IsNullOrWhiteSpace(jwtToken))
            throw InfrastructureErrors.General.ErrorMessage("JWT token is not found");

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);

        var sessionIdStr = token.Claims.FirstOrDefault(c => c.Type == "sessionId")?.Value;

        if (string.IsNullOrWhiteSpace(sessionIdStr))
            throw InfrastructureErrors.General.ErrorMessage("SessionId in JWT token is not found");

        var sessionId = Guid.Parse(sessionIdStr);

        var session = await mediator.Send(
            new FindSessionByIdQuery(sessionId),
            cancellationToken);

        if (session is null)
            throw DomainErrors.Session.NotFound();

        return session.UserId;
    }
}