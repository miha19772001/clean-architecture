namespace Backend.Application.APIHandlers.Product.GetFilteredList;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Queries.Product;

internal sealed class GetFilteredListRequestHandler(
    IMediator mediator)
    : IRequestHandler<GetFilteredListRequest, GetFilteredListResponse>
{
    public async Task<GetFilteredListResponse> Handle(
        GetFilteredListRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new FindProductByFilterQuery(
                request.Pagination),
            cancellationToken);

        return new GetFilteredListResponse(result);
    }
}