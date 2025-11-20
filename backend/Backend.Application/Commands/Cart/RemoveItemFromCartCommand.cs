namespace Backend.Application.Commands.Cart;

using Backend.Application.Mapping.Cart;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.DTOs.Cart;
using Infrastructure.PostgreSQL;
using MediatR;
using Domain.AggregatesModel.CartAggregate;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;
using System;

public sealed record RemoveItemFromCartCommand(
    Guid UserId,
    Guid ProductId)
    : IRequest<CartItemDto>;

internal sealed class RemoveItemFromCartCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<RemoveItemFromCartCommand, CartItemDto>
{
    public async Task<CartItemDto> Handle(
        RemoveItemFromCartCommand request,
        CancellationToken cancellationToken = default)
    {
        var cart = await context.Set<Cart>()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId,
                cancellationToken);

        if (cart is null)
            throw DomainErrors.Cart.NotFound();

        var cartItem = cart.RemoveItem(request.ProductId);

        await context.SaveChangesAsync(cancellationToken);

        return CartItemMapper.MapToCartItemDto(cartItem);
    }
}