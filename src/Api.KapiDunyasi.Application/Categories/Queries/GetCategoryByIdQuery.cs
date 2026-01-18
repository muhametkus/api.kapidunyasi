using Api.KapiDunyasi.Application.Categories.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Categories.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryResponseDto?>;
