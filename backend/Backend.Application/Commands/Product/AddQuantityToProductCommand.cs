namespace Backend.Application.Commands.Product;

using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.DTOs.Product;
using Backend.Application.Mapping.Product;
using Domain.Errors;
using Infrastructure.PostgreSQL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.AggregatesModel.ProductAggregate;

public sealed record AddQuantityToProductCommand(
    Guid Id)
    : IRequest<ProductDto>;

internal sealed class AddQuantityToProductCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<AddQuantityToProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(
        AddQuantityToProductCommand request,
        CancellationToken cancellationToken = default)
    {
        var product = await context.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        if (product is null)
            throw DomainErrors.Product.NotFound();

        product.AddQuantity();

        await context.SaveChangesAsync(cancellationToken);

        return ProductMapper.MapToProductDto(product);
    }
}