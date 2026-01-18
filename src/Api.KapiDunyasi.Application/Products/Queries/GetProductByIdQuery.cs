using Api.KapiDunyasi.Application.Products.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDetailDto?>;
