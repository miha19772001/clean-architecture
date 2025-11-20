namespace Backend.Application.DTOs.Product;

using System;

public sealed record ProductDto(
    Guid Id,
    string Name,
    bool IsDeleted,
    string Description,
    decimal Price,
    int Quantity,
    Guid ImageId,
    DateTime CreatedAt);