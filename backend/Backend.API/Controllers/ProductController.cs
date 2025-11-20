namespace Backend.API.Controllers;

using Filters;
using Domain.Errors;
using Common;
using Application.APIHandlers.Product.GetFilteredList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/product")]
public class ProductController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost("getFilteredList")]
    [RoleFilter()]
    [ProducesResponseType(typeof(GetFilteredListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFilteredList(
        [FromBody] GetFilteredListRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<GetFilteredListRequest, GetFilteredListResponse>(request, cancellationToken);
    }
}