using MediatR;

namespace Api.KapiDunyasi.Application.Categories.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;
