namespace Backend.Application.APIHandlers.Cart.GetCartItems;

using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Queries.Cart;
using Backend.Application.Services.Account;
using MediatR;

internal sealed class GetCartItemsRequestHandler(
    IMediator mediator,
    IAccountService accountService)
    : IRequestHandler<GetCartItemsRequest, GetCartItemsResponse>
{
    public async Task<GetCartItemsResponse> Handle(
        GetCartItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = await accountService.GetCurrentUserIdAsync(cancellationToken);

        var cartItems = await mediator.Send(
            new FindCartItemsByUserIdQuery(
                userId),
            cancellationToken);

        return new GetCartItemsResponse(cartItems);
    }
}