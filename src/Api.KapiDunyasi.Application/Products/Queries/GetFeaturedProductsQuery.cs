using Api.KapiDunyasi.Application.Products.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Products.Queries;

public record GetFeaturedProductsQuery() : IRequest<List<ProductListDto>>;
