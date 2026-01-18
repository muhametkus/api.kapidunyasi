using Api.KapiDunyasi.Application.Users.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<UserResponseDto?>;
