namespace Backend.API.Controllers;

using Application.APIHandlers.Cart.AddItem;
using Application.APIHandlers.Cart.RemoveItem;
using Application.APIHandlers.Cart.GetFilteredList;
using Application.APIHandlers.Cart.GetCartItems;
using Filters;
using Domain.Errors;
using Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cart")]
public class CartController(IMediator mediator) : ApiControllerBase(mediator)
{
    [HttpPost("addItem")]
    [RoleFilter(RequiredRole.User)]
    [ProducesResponseType(typeof(AddItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItem(
        [FromBody] AddItemRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<AddItemRequest, AddItemResponse>(request, cancellationToken);
    }

    [HttpPost("removeItem")]
    [RoleFilter(RequiredRole.User)]
    [ProducesResponseType(typeof(RemoveItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveItem(
        [FromBody] RemoveItemRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<RemoveItemRequest, RemoveItemResponse>(request, cancellationToken);
    }

    [HttpPost("getFilteredList")]
    [RoleFilter(RequiredRole.User)]
    [ProducesResponseType(typeof(GetFilteredListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFilteredList(
        [FromBody] GetFilteredListRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<GetFilteredListRequest, GetFilteredListResponse>(request, cancellationToken);
    }

    [HttpPost("getCartItems")]
    [RoleFilter(RequiredRole.User)]
    [ProducesResponseType(typeof(GetCartItemsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCartItems(
        [FromBody] GetCartItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        return await ProcessAsync<GetCartItemsRequest, GetCartItemsResponse>(request, cancellationToken);
    }
}