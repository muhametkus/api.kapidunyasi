using MediatR;

namespace Api.KapiDunyasi.Application.Users.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<Unit>;
