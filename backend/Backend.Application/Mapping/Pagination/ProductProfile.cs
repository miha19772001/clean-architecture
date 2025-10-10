namespace Backend.Application.Mapping.Pagination;

using Backend.Application.Common.Pagination;
using AutoMapper;

public class PaginationProfile : Profile
{
    public PaginationProfile()
    {
        CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
    }
}