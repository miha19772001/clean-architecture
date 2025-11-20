namespace Backend.Application.Commands.Cart;

using Backend.Application.Mapping.Cart;
using Backend.Application.DTOs.Cart;
using MediatR;
using System;
using Domain.AggregatesModel.CartAggregate;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.PostgreSQL;

public sealed record CreateCartCommand(
    Guid UserId)
    : IRequest<CartDto>;

internal sealed class CreateCartCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<CreateCartCommand, CartDto>
{
    public async Task<CartDto> Handle(
        CreateCartCommand request,
        CancellationToken cancellationToken = default)
    {
        var cart = Cart.Create(request.UserId);

        await context.Set<Cart>().AddAsync(cart, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return CartMapper.MapToCartDto(cart);
    }
}