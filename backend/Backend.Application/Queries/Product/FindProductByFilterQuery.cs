namespace Backend.Application.Queries.Product;

using Microsoft.EntityFrameworkCore;
using Backend.Domain.Common;
using Common.Pagination;
using MediatR;
using Domain.AggregatesModel.ProductAggregate;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public record FindProductByFilterQuery(Pagination Pagination) : IRequest<PaginatedResult<Product>>;

public class FindProductByFilterQueryHandler(IRepository<Product> repository)
    : IRequestHandler<FindProductByFilterQuery, PaginatedResult<Product>>
{
    public async Task<PaginatedResult<Product>> Handle(
        FindProductByFilterQuery request,
        CancellationToken cancellationToken = default)
    {
        var query = repository.CreateQuery();
    
        var totalCount = await query.CountAsync(cancellationToken);
    
        var products = await query
            .Skip(request.Pagination.Offset)
            .Take(request.Pagination.Count)
            .ToListAsync(cancellationToken);
    
        return new PaginatedResult<Product>(products, totalCount);
    }
}