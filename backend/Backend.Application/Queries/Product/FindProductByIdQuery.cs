namespace Backend.Application.Queries.Product;

using Backend.Application.Mapping.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.AggregatesModel.ProductAggregate;
using Backend.Application.DTOs.Product;
using Infrastructure.PostgreSQL;

public sealed record FindProductByIdQuery(
    Guid Id)
    : IRequest<ProductDto>;

internal sealed class FindProductByIdQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(
        FindProductByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var product = await context.Set<Product>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        return ProductMapper.MapToProductDto(product);
    }
}