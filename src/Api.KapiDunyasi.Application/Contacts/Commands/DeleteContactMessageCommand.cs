using MediatR;

namespace Api.KapiDunyasi.Application.Contacts.Commands;

public record DeleteContactMessageCommand(Guid Id) : IRequest<Unit>;
