namespace Backend.Application.APIHandlers.Cart.GetFilteredList;

using Backend.Application.Queries.Cart;
using Backend.Application.Services.Account;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

internal sealed class GetFilteredListRequestHandler(
    IMediator mediator,
    IAccountService accountService)
    : IRequestHandler<GetFilteredListRequest, GetFilteredListResponse>
{
    public async Task<GetFilteredListResponse> Handle(
        GetFilteredListRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = await accountService.GetCurrentUserIdAsync(cancellationToken);

        var result = await mediator.Send(
            new FindCartItemsByFilterAndUserIdQuery(
                request.Pagination,
                userId),
            cancellationToken);

        return new GetFilteredListResponse(result);
    }
}