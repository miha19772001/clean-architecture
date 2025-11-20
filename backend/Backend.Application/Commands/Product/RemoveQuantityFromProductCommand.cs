namespace Backend.Application.Commands.Product;

using Backend.Application.Mapping.Product;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.DTOs.Product;
using Infrastructure.PostgreSQL;
using MediatR;
using Domain.AggregatesModel.ProductAggregate;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

public sealed record RemoveQuantityFromProductCommand(
    Guid Id)
    : IRequest<ProductDto>;

internal sealed class RemoveQuantityFromProductCommandHandler(
    ApplicationDbContext context)
    : IRequestHandler<RemoveQuantityFromProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(
        RemoveQuantityFromProductCommand request,
        CancellationToken cancellationToken = default)
    {
        var product = await context.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        if (product is null)
            throw DomainErrors.Product.NotFound();

        product.RemoveQuantity();

        await context.SaveChangesAsync(cancellationToken);

        return ProductMapper.MapToProductDto(product);
    }
}