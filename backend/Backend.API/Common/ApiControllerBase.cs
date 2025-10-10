namespace Backend.API.Common;

using MediatR;
using Microsoft.AspNetCore.Mvc;

public class ApiControllerBase : ControllerBase
{
    private readonly IMediator _mediator;

    protected ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected async Task<TResponse> ProcessAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        return await _mediator.Send(request, cancellationToken);
    }
}