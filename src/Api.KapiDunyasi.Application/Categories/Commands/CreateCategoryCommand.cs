using Api.KapiDunyasi.Application.Categories.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Categories.Commands;

public record CreateCategoryCommand(CategoryCreateRequestDto Payload) : IRequest<Guid>;
