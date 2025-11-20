namespace Backend.API.Controllers;

using Common;
using Filters;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.APIHandlers.File.GetImage;

[ApiController]
[Route("api/file")]
public class FileController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpGet("getImage")]
    [RoleFilter()]
    [ProducesResponseType(typeof(PhysicalFileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetImage(
        [FromQuery] Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetImageRequest(id);
        return await ProcessFileAsync<GetImageRequest, PhysicalFileResult>(request, cancellationToken);
    }
}