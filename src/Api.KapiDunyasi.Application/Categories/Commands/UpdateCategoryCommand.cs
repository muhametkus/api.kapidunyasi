using Api.KapiDunyasi.Application.Categories.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Categories.Commands;

public record UpdateCategoryCommand(Guid Id, CategoryUpdateRequestDto Payload) : IRequest<Unit>;
