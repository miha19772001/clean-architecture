namespace Backend.Application.APIHandlers.Cart.AddItem;

using Backend.Application.DTOs.Cart;
using System;
using MediatR;

public sealed record AddItemRequest(
    Guid ProductId)
    : IRequest<AddItemResponse>;

public sealed record AddItemResponse(
    CartItemDto CartItem);