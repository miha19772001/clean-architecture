namespace Backend.Application.APIHandlers.Cart.GetCartItems;

using System.Collections.Generic;
using Backend.Application.DTOs.Cart;
using MediatR;

public sealed record GetCartItemsRequest()
    : IRequest<GetCartItemsResponse>;

public sealed record GetCartItemsResponse(
    IReadOnlyList<CartItemDto> CartItems);