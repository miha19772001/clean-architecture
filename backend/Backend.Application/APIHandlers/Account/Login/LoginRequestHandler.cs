namespace Backend.Application.APIHandlers.Account.Login;

using Infrastructure.PasswordHasher;
using System.Threading;
using System.Threading.Tasks;
using Queries.User;
using Backend.Application.Services.Account;
using Domain.Errors;
using MediatR;

internal sealed class LoginRequestHandler(
    IAccountService accountService,
    IPasswordHasher passwordHasher,
    IMediator mediator)
    : IRequestHandler<LoginRequest, LoginResponse>
{
    public async Task<LoginResponse> Handle(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await mediator.Send(
            new FindUserByLoginQuery(
                request.Login),
            cancellationToken);

        if (user is null || user.IsDeleted)
            throw DomainErrors.User.NotFound();

        if (!passwordHasher.Verify(request.Password, user.PasswordHash))
            throw DomainErrors.User.WrongPassword();

        await accountService.SignInAsync(user.Id, cancellationToken);

        return new LoginResponse();
    }
}