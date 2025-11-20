namespace Backend.Application.APIHandlers.Cart.GetFilteredList;

using Common.Pagination;
using Backend.Application.DTOs.Cart;
using MediatR;

public sealed record GetFilteredListRequest(
    Pagination Pagination)
    : IRequest<GetFilteredListResponse>;

public sealed record GetFilteredListResponse(
    PaginatedResult<CartItemDto> Result);