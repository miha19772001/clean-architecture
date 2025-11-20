namespace Backend.Application.APIHandlers.Account.Logout;

using MediatR;

public sealed record LogoutRequest()
    : IRequest<LogoutResponse>;

public sealed record LogoutResponse();