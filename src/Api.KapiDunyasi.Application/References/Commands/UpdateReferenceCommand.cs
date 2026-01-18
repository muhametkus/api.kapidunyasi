using Api.KapiDunyasi.Application.References.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.References.Commands;

public record UpdateReferenceCommand(Guid Id, ReferenceUpdateRequestDto Payload) : IRequest<Unit>;
