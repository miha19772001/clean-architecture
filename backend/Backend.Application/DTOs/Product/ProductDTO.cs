namespace Backend.Application.DTOs.Product;

using System;

public record ProductDTO(
    int Id,
    string Name,
    bool IsDeleted,
    string Description,
    decimal Price,
    int Quantity,
    int ImageId,
    DateTime CreatedAt);