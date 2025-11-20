namespace Backend.Application.Mapping.Product;

using Backend.Application.DTOs.Product;
using Domain.AggregatesModel.ProductAggregate;

public static class ProductMapper
{
    public static ProductDto MapToProductDto(Product product)
    {
        return new ProductDto(
            Id: product.Id,
            Name: product.Name,
            IsDeleted: product.IsDeleted,
            Description: product.Description,
            Price: product.Price,
            Quantity: product.Quantity,
            ImageId: product.ImageId,
            CreatedAt: product.CreatedAt);
    }
}