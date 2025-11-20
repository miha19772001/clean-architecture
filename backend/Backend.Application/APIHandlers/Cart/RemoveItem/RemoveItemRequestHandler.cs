namespace Backend.Application.APIHandlers.Cart.RemoveItem;

using System.Linq;
using Backend.Application.Queries.Cart;
using Backend.Application.Commands.Product;
using Domain.Errors;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Commands.Cart;
using MediatR;
using Backend.Application.Services.Account;

public class RemoveItemRequestHandler(
    IAccountService accountService,
    IMediator mediator)
    : IRequestHandler<RemoveItemRequest, RemoveItemResponse>
{
    public async Task<RemoveItemResponse> Handle(
        RemoveItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = await accountService.GetCurrentUserIdAsync(cancellationToken);

        var cart = await mediator.Send(
            new FindCartByUserIdQuery(
                userId),
            cancellationToken);

        var item = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);

        if (item is null)
            throw DomainErrors.Cart.ItemNotFound();

        await mediator.Send(
            new AddQuantityToProductCommand(
                request.ProductId),
            cancellationToken);

        var cartItem = await mediator.Send(
            new RemoveItemFromCartCommand(
                userId,
                request.ProductId),
            cancellationToken);

        return new RemoveItemResponse(cartItem);
    }
}