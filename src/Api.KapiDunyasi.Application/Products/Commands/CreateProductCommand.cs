using Api.KapiDunyasi.Application.Products.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Products.Commands;

public record CreateProductCommand(ProductCreateRequestDto Payload) : IRequest<Guid>;
