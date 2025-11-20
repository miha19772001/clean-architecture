namespace Backend.API.Common;

using Infrastructure.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class ApiControllerBase : ControllerBase
{
    private readonly IMediator _mediator;

    protected ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected async Task<IActionResult> ProcessAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    protected async Task<IActionResult> ProcessFileAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response switch
        {
            FileStreamResult fileStream => fileStream,
            FileContentResult fileContent => fileContent,
            PhysicalFileResult physicalFile => physicalFile,
            VirtualFileResult virtualFile => virtualFile,
            _ => throw InfrastructureErrors.General.ErrorMessage("Response is not a valid file result type")
        };
    }
}