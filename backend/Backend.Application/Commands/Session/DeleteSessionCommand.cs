namespace Backend.Application.Commands.Session;

using Backend.Application.Mapping.Session;
using System;
using Domain.Errors;
using Backend.Application.DTOs.Session;
using Infrastructure.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.AggregatesModel.SessionAggregate;

public sealed record DeleteSessionCommand(
    Guid Id)
    : IRequest<SessionDto>;

internal sealed class DeleteSessionCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<DeleteSessionCommand, SessionDto>
{
    public async Task<SessionDto> Handle(
        DeleteSessionCommand request,
        CancellationToken cancellationToken = default)
    {
        var session = await context.Set<Session>()
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        if (session is null)
            throw DomainErrors.Session.NotFound();

        session.SetIsDeleted();

        await context.SaveChangesAsync(cancellationToken);

        return SessionMapper.MapToSessionDto(session);
    }
}