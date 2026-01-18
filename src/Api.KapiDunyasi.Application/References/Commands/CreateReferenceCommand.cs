using Api.KapiDunyasi.Application.References.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.References.Commands;

public record CreateReferenceCommand(ReferenceCreateRequestDto Payload) : IRequest<Guid>;
