namespace Backend.Application.Queries.User;

using Backend.Application.Mapping.User;
using Backend.Application.DTOs.User;
using Infrastructure.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.AggregatesModel.UserAggregate;

public sealed record FindUserByLoginQuery(
    string Login)
    : IRequest<UserWithPasswordHashDto>;

internal sealed class FindUserByLoginQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindUserByLoginQuery, UserWithPasswordHashDto>
{
    public async Task<UserWithPasswordHashDto> Handle(
        FindUserByLoginQuery request,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Set<User>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == request.Login,
                cancellationToken);

        return UserMapper.MapToUserWithPasswordHashDto(user);
    }
}