using MediatR;

namespace Api.KapiDunyasi.Application.References.Commands;

public record DeleteReferenceCommand(Guid Id) : IRequest<Unit>;
