namespace Backend.API.Controllers;

using Application.APIHandlers.Account.GetCurrent;
using Application.APIHandlers.Account.Logout;
using Application.APIHandlers.Account.Login;
using Application.APIHandlers.Account.Register;
using Common;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Filters;

[ApiController]
[Route("api/account")]
public class AccountController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost("getCurrent")]
    [RoleFilter()]
    [ProducesResponseType(typeof(GetCurrentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCurrent(
        [FromBody] GetCurrentRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<GetCurrentRequest, GetCurrentResponse>(request, cancellationToken);
    }

    [HttpPost("register")]
    [RoleFilter()]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<RegisterRequest, RegisterResponse>(request, cancellationToken);
    }

    [HttpPost("login")]
    [RoleFilter()]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<LoginRequest, LoginResponse>(request, cancellationToken);
    }

    [HttpPost("logout")]
    [RoleFilter(RequiredRole.User)]
    [ProducesResponseType(typeof(LogoutResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<LogoutRequest, LogoutResponse>(request, cancellationToken);
    }
}