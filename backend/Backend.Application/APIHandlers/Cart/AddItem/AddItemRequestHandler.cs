namespace Backend.Application.APIHandlers.Cart.AddItem;

using Backend.Application.Commands.Product;
using Backend.Application.Queries.Product;
using Domain.Errors;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Commands.Cart;
using MediatR;
using Backend.Application.Services.Account;

public class AddItemRequestHandler(
    IAccountService accountService,
    IMediator mediator)
    : IRequestHandler<AddItemRequest, AddItemResponse>
{
    public async Task<AddItemResponse> Handle(
        AddItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = await accountService.GetCurrentUserIdAsync(cancellationToken);

        var product = await mediator.Send(
            new FindProductByIdQuery(
                request.ProductId),
            cancellationToken);

        if (product.Quantity == 0)
            throw DomainErrors.Product.ProductIsOutOfStock();

        await mediator.Send(
            new RemoveQuantityFromProductCommand(
                request.ProductId),
            cancellationToken);

        var cartItem = await mediator.Send(
            new AddItemToCartCommand(
                userId,
                request.ProductId),
            cancellationToken);

        return new AddItemResponse(cartItem);
    }
}