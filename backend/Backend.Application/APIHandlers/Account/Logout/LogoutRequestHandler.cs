namespace Backend.Application.APIHandlers.Account.Logout;

using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Services.Account;
using MediatR;

internal sealed class LogoutRequestHandler(
    IAccountService accountService)
    : IRequestHandler<LogoutRequest, LogoutResponse>
{
    public async Task<LogoutResponse> Handle(
        LogoutRequest request,
        CancellationToken cancellationToken = default)
    {
        await accountService.SignOutAsync(cancellationToken);

        return new LogoutResponse();
    }
}