using Api.KapiDunyasi.Application.Products.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Products.Queries;

public record GetRelatedProductsQuery(Guid? CategoryId, string? Series, Guid? ExcludeId, int Take = 8)
    : IRequest<List<ProductListDto>>;
