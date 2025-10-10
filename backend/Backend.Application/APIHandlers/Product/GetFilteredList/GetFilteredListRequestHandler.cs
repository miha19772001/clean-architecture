namespace Backend.Application.APIHandlers.Product.GetFilteredList;

using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Common.Pagination;
using Backend.Application.DTOs.Product;
using Backend.Application.Queries.Product;

public class GetFilteredListRequestHandler(
    IMediator mediator,
    IMapper mapper)
    : IRequestHandler<GetFilteredListRequest, GetFilteredListResponse>
{
    public async Task<GetFilteredListResponse> Handle(
        GetFilteredListRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator
            .Send(new FindProductByFilterQuery(request.Pagination), cancellationToken);

        return new GetFilteredListResponse(mapper.Map<PaginatedResult<ProductDTO>>(result));
    }
}