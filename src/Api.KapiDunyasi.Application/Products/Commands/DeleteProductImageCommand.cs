using MediatR;

namespace Api.KapiDunyasi.Application.Products.Commands;

public record DeleteProductImageCommand(Guid ProductId, string ImageUrl) : IRequest<Unit>;
