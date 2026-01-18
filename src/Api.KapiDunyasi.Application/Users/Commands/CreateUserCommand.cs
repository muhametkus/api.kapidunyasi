using Api.KapiDunyasi.Application.Users.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Users.Commands;

public record CreateUserCommand(UserCreateRequestDto Payload) : IRequest<Guid>;
