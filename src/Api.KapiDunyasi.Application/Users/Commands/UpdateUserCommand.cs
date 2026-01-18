using Api.KapiDunyasi.Application.Users.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Users.Commands;

public record UpdateUserCommand(Guid Id, UserUpdateRequestDto Payload, Guid RequestingUserId) : IRequest<Unit>;
