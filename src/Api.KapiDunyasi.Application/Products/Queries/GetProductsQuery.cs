using Api.KapiDunyasi.Application.Common.Models;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Requests;
using MediatR;

namespace Api.KapiDunyasi.Application.Products.Queries;

public record GetProductsQuery(ProductListRequest Request) : IRequest<PagedResult<ProductListDto>>;
