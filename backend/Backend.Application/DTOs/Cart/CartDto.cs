namespace Backend.Application.DTOs.Cart;

using System;
using System.Collections.Generic;
using Domain.AggregatesModel.CartAggregate;

public sealed record CartDto(
    Guid Id,
    Guid UserId,
    IReadOnlyList<CartItem> Items);