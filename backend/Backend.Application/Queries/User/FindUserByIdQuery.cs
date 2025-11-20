namespace Backend.Application.Queries.User;

using Backend.Application.Mapping.User;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.AggregatesModel.UserAggregate;
using Backend.Application.DTOs.User;
using Infrastructure.PostgreSQL;

public sealed record FindUserByIdQuery(
    Guid Id)
    : IRequest<UserDto>;

internal sealed class FindUserByIdQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(
        FindUserByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Set<User>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        return UserMapper.MapToUserDto(user);
    }
}