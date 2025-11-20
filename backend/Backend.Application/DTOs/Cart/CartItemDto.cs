namespace Backend.Application.DTOs.Cart;

using System;

public sealed record CartItemDto(
    Guid Id,
    Guid ProductId,
    int Quantity);