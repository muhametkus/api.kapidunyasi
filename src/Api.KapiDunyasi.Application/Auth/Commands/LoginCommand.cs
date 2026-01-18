using Api.KapiDunyasi.Application.Auth.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Auth.Commands;

public record LoginCommand(LoginRequestDto Payload) : IRequest<AuthResponseDto>;
