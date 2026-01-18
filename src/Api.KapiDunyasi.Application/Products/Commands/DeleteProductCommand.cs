using MediatR;

namespace Api.KapiDunyasi.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<Unit>;
