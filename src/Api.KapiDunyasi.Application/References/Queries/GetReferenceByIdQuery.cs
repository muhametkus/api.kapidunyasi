using Api.KapiDunyasi.Application.References.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.References.Queries;

public record GetReferenceByIdQuery(Guid Id) : IRequest<ReferenceResponseDto?>;
