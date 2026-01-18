using Api.KapiDunyasi.Application.Products.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Products.Commands;

public record UpdateProductCommand(Guid Id, ProductUpdateRequestDto Payload) : IRequest<Unit>;
