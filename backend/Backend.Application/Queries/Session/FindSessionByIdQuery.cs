namespace Backend.Application.Queries.Session;

using Backend.Application.Mapping.Session;
using Infrastructure.PostgreSQL;
using Backend.Application.DTOs.Session;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.AggregatesModel.SessionAggregate;

public sealed record FindSessionByIdQuery(
    Guid Id)
    : IRequest<SessionDto>;

internal sealed class FindSessionByIdQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindSessionByIdQuery, SessionDto>
{
    public async Task<SessionDto> Handle(
        FindSessionByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var session = await context.Set<Session>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        return SessionMapper.MapToSessionDto(session);
    }
}