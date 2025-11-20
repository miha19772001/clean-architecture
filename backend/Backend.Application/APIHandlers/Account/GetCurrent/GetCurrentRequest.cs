namespace Backend.Application.APIHandlers.Account.GetCurrent;

using DTOs.User;
using MediatR;

public sealed record GetCurrentRequest() 
    : IRequest<GetCurrentResponse>;

public sealed record GetCurrentResponse(
    UserDto User);