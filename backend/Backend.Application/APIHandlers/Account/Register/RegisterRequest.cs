namespace Backend.Application.APIHandlers.Account.Register;

using MediatR;

public sealed record RegisterRequest(
    string Login,
    string Password)
    : IRequest<RegisterResponse>;

public sealed record RegisterResponse();