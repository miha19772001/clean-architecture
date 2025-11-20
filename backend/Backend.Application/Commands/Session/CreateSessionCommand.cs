namespace Backend.Application.Commands.Session;

using Backend.Application.Mapping.Session;
using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.AggregatesModel.SessionAggregate;
using Backend.Application.DTOs.Session;
using Infrastructure.PostgreSQL;

public sealed record CreateSessionCommand(
    Guid UserId,
    string Ip,
    string Agent)
    : IRequest<SessionDto>;

internal sealed class CreateSessionCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<CreateSessionCommand, SessionDto>
{
    public async Task<SessionDto> Handle(
        CreateSessionCommand request,
        CancellationToken cancellationToken = default)
    {
        var session = Session.Create(request.UserId, request.Ip, request.Agent);

        await context.Set<Session>().AddAsync(session, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return SessionMapper.MapToSessionDto(session);
    }
}