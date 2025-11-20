namespace Backend.Application.Queries.Cart;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.DTOs.Cart;
using Backend.Application.Mapping.Cart;
using Infrastructure.PostgreSQL;
using MediatR;
using Domain.AggregatesModel.CartAggregate;

public sealed record FindCartItemsByUserIdQuery(
    Guid UserId)
    : IRequest<IReadOnlyList<CartItemDto>>;

internal sealed class FindCartItemsByUserIdQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindCartItemsByUserIdQuery, IReadOnlyList<CartItemDto>>
{
    public async Task<IReadOnlyList<CartItemDto>> Handle(
        FindCartItemsByUserIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var cart = await context.Set<Cart>().AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(x => x.UserId == request.UserId,
                cancellationToken);

        if (cart is null)
            return [];

        return cart.Items
            .Select(CartItemMapper.MapToCartItemDto)
            .ToList();
    }
}