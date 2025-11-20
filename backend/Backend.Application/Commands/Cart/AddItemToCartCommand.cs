namespace Backend.Application.Commands.Cart;

using Backend.Application.DTOs.Cart;
using MediatR;
using System.Threading;
using System;
using System.Threading.Tasks;
using Backend.Application.Mapping.Cart;
using Infrastructure.PostgreSQL;
using Domain.AggregatesModel.CartAggregate;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

public sealed record AddItemToCartCommand(
    Guid UserId,
    Guid ProductId)
    : IRequest<CartItemDto>;

internal sealed class AddItemToCartCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<AddItemToCartCommand, CartItemDto>
{
    public async Task<CartItemDto> Handle(
        AddItemToCartCommand request,
        CancellationToken cancellationToken = default)
    {
        var cart = await context.Set<Cart>()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId,
                cancellationToken);

        if (cart is null)
            throw DomainErrors.Cart.NotFound();

        var cartItem = cart.AddItem(request.ProductId);

        await context.SaveChangesAsync(cancellationToken);

        return CartItemMapper.MapToCartItemDto(cartItem);
    }
}