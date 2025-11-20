namespace Backend.Application.Queries.Cart;

using System;
using Backend.Application.Mapping.Cart;
using Domain.Errors;
using Backend.Application.DTOs.Cart;
using Microsoft.EntityFrameworkCore;
using Common.Pagination;
using MediatR;
using Domain.AggregatesModel.CartAggregate;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.PostgreSQL;

public sealed record FindCartItemsByFilterAndUserIdQuery(
    Pagination Pagination,
    Guid UserId)
    : IRequest<PaginatedResult<CartItemDto>>;

internal sealed class FindCartItemByFilterQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindCartItemsByFilterAndUserIdQuery, PaginatedResult<CartItemDto>>
{
    public async Task<PaginatedResult<CartItemDto>> Handle(
        FindCartItemsByFilterAndUserIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var cart = await context.Set<Cart>().AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(x => x.UserId == request.UserId,
                cancellationToken);

        if (cart is null)
            throw DomainErrors.Cart.NotFound();

        var totalCount = cart.Items.Count;

        var cartItems = cart.Items
            .Skip(request.Pagination.Offset)
            .Take(request.Pagination.Count)
            .Select(CartItemMapper.MapToCartItemDto)
            .ToList();

        return new PaginatedResult<CartItemDto>(cartItems, totalCount);
    }
}