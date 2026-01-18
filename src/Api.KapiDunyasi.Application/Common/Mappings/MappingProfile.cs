using Api.KapiDunyasi.Application.Categories.Dtos;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Dtos.Shared;
using Api.KapiDunyasi.Domain.Categories;
using Api.KapiDunyasi.Domain.Products;
using AutoMapper;

namespace Api.KapiDunyasi.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryResponseDto>();

        CreateMap<ProductPrice, ProductPriceDto>();
        CreateMap<ProductSpecs, ProductSpecsDto>();

        CreateMap<Product, ProductListDto>();
        CreateMap<Product, ProductDetailDto>();
    }
}
