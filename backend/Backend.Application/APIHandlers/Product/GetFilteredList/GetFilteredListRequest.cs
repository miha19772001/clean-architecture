namespace Backend.Application.APIHandlers.Product.GetFilteredList;

using MediatR;
using Common.Pagination;
using Backend.Application.DTOs.Product;

public record GetFilteredListRequest(Pagination Pagination) : IRequest<GetFilteredListResponse>;

public record GetFilteredListResponse(PaginatedResult<ProductDTO> Result);