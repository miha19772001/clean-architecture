namespace Backend.Application.APIHandlers.Account.Login;

using MediatR;

public sealed record LoginRequest(
    string Login,
    string Password)
    : IRequest<LoginResponse>;

public sealed record LoginResponse();