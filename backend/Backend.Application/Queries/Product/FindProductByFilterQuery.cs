namespace Backend.Application.Queries.Product;

using Backend.Application.Mapping.Product;
using Backend.Application.DTOs.Product;
using Microsoft.EntityFrameworkCore;
using Common.Pagination;
using MediatR;
using Domain.AggregatesModel.ProductAggregate;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.PostgreSQL;

public sealed record FindProductByFilterQuery(
    Pagination Pagination)
    : IRequest<PaginatedResult<ProductDto>>;

internal sealed class FindProductByFilterQueryHandler(
    ApplicationDbContext context)
    : IRequestHandler<FindProductByFilterQuery, PaginatedResult<ProductDto>>
{
    public async Task<PaginatedResult<ProductDto>> Handle(
        FindProductByFilterQuery request,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Product>().AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var products = query
            .Skip(request.Pagination.Offset)
            .Take(request.Pagination.Count)
            .AsEnumerable()
            .Select(ProductMapper.MapToProductDto)
            .ToList();

        return new PaginatedResult<ProductDto>(products, totalCount);
    }
}