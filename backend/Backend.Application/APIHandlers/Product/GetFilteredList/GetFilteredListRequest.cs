namespace Backend.Application.APIHandlers.Product.GetFilteredList;

using MediatR;
using Common.Pagination;
using Backend.Application.DTOs.Product;

public sealed record GetFilteredListRequest(
    Pagination Pagination)
    : IRequest<GetFilteredListResponse>;

public sealed record GetFilteredListResponse(
    PaginatedResult<ProductDto> Result);