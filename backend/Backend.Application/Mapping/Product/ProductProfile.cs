namespace Backend.Application.Mapping.Product;

using AutoMapper;
using Backend.Application.DTOs.Product;
using Domain.AggregatesModel.ProductAggregate;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>();
    }
}