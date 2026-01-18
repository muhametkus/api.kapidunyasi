using Api.KapiDunyasi.Application.Users.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Users.Commands;

public record AdminUpdateUserCommand(Guid Id, AdminUserUpdateRequestDto Payload) : IRequest<Unit>;
