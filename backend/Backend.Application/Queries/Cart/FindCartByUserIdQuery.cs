namespace Backend.Application.Queries.Cart;

using Backend.Application.Mapping.Cart;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.DTOs.Cart;
using Infrastructure.PostgreSQL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.AggregatesModel.CartAggregate;

public sealed record FindCartByUserIdQuery(
    Guid UserId)
    : IRequest<CartDto>;

internal sealed class FindCartByUserIdQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindCartByUserIdQuery, CartDto>
{
    public async Task<CartDto> Handle(
        FindCartByUserIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var cart = await context.Set<Cart>().AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(x => x.UserId == request.UserId,
                cancellationToken);

        return CartMapper.MapToCartDto(cart);
    }
}