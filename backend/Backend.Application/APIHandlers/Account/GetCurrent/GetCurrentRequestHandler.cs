namespace Backend.Application.APIHandlers.Account.GetCurrent;

using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Services.Account;
using MediatR;
using Domain.Errors;
using Queries.User;

internal sealed class GetCurrentRequestHandler(
    IAccountService accountService,
    IMediator mediator)
    : IRequestHandler<GetCurrentRequest, GetCurrentResponse>
{
    public async Task<GetCurrentResponse> Handle(
        GetCurrentRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = await accountService.GetCurrentUserIdAsync(cancellationToken);

        var user = await mediator.Send(
            new FindUserByIdQuery(
                userId),
            cancellationToken);

        if (user is null)
            throw DomainErrors.User.NotFound();

        return new GetCurrentResponse(user);
    }
}