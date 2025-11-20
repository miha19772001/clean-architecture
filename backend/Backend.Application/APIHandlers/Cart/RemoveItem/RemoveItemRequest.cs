namespace Backend.Application.APIHandlers.Cart.RemoveItem;

using Backend.Application.DTOs.Cart;
using System;
using MediatR;

public sealed record RemoveItemRequest(
    Guid ProductId)
    : IRequest<RemoveItemResponse>;

public sealed record RemoveItemResponse(
    CartItemDto CartItem);