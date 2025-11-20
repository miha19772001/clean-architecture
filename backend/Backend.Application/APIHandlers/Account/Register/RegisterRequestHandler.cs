namespace Backend.Application.APIHandlers.Account.Register;

using Backend.Application.Commands.Cart;
using Services.Account;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Commands.User;
using Queries.User;
using Domain.Errors;
using Infrastructure.PasswordHasher;

internal sealed class RegisterRequestHandler(
    IAccountService accountService,
    IPasswordHasher passwordHasher,
    IMediator mediator)
    : IRequestHandler<RegisterRequest, RegisterResponse>
{
    public async Task<RegisterResponse> Handle(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await mediator.Send(
            new FindUserByLoginQuery(
                request.Login),
            cancellationToken);

        if (user is not null)
            throw DomainErrors.User.HasUserWithSameLogin();

        var hashedPassword = passwordHasher.Generate(request.Password);

        var newUser = await mediator.Send(
            new CreateUserCommand(
                request.Login,
                hashedPassword),
            cancellationToken);

        await mediator.Send(
            new CreateCartCommand(
                newUser.Id),
            cancellationToken);

        await accountService.SignInAsync(newUser.Id, cancellationToken);

        return new RegisterResponse();
    }
}