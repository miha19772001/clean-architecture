namespace Backend.Application.Commands.User;

using Backend.Application.Mapping.User;
using System.Threading;
using System.Threading.Tasks;
using Domain.AggregatesModel.UserAggregate;
using MediatR;
using Backend.Application.DTOs.User;
using Infrastructure.PostgreSQL;

public sealed record CreateUserCommand(
    string Login,
    string Password)
    : IRequest<UserDto>;

internal sealed class CreateUserCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        var user = User.Create(request.Login, request.Password);

        await context.Set<User>().AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return UserMapper.MapToUserDto(user);
    }
}