namespace Backend.Application.Queries.File;

using Backend.Application.Mapping.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.DTOs.File;
using File = Domain.AggregatesModel.FileAggregate.File;
using Infrastructure.PostgreSQL;
using MediatR;

public sealed record FindFileByIdQuery(
    Guid Id)
    : IRequest<FileDto>;

internal sealed class FindFileByIdQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindFileByIdQuery, FileDto>
{
    public async Task<FileDto> Handle(
        FindFileByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var file = await context.Set<File>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        return FileMapper.MapToFileDto(file);
    }
}